using AutoMapper;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Ticketing.Core;
using Ticketing.Core.Entities;
using TicketingService.DTOs;
using Status = TicketingService.DTOs.Status;

namespace TicketingService
{
    public class TicketingService : ITicketingService
    {
        private readonly ITicketingRepository _repository;

        private readonly IConnectionMultiplexer _redisServer;

        private readonly IMapper _mapper;
        private readonly IDatabase _db;

        public TicketingService(ITicketingRepository repository, IConnectionMultiplexer redisServer, IMapper mapper)
        {
            _repository = repository;
            _redisServer = redisServer;
            _mapper = mapper;
            _db = redisServer.GetDatabase();
        }

        public async Task<int> CreateReservation(BookTicketDTO booking)
        {
            Booking ent = _mapper.Map<BookTicketDTO, Booking>(booking);
            await _repository.CreateReservation(ent);
            return ent.BookingId;
        }

        public async Task<List<TicketDTO>> GetAllTickets(int eventId)
        {
            var ent = await _repository.GetAllEvents(eventId);
            return _mapper.Map<List<EventTicketInventory>, List<TicketDTO>>(ent);
        }

        public async Task<int> GetAvailableTicketsForTier(int eventId, string tierName)
        {
            String key = $"event:{eventId}:tier:{tierName}:stock";
            var value = await _db.StringGetAsync(key);
            return ((int)value);

        }

        public async Task<BookTicketDTO> GetBooking(int id)
        {
            Booking bk = await _repository.GetBooking(id);
            BookTicketDTO dto = _mapper.Map<Booking, BookTicketDTO>(bk);
            return dto;
        }

        public async Task<bool> ReserveTicketsForUser(BookTicketDTO booking)
        {
            bool allReserved = true;
            var failedTiers = new List<TicketTierReservation>();

            // List to store the reservations of successful tiers (to rollback if any tier fails)
            var reservedTiers = new List<TicketTierReservation>();

            // Loop through each tier and reserve the corresponding number of tickets
            foreach (var tier in booking.reservationData)
            {
                var availableStock = await GetAvailableTicketsForTier(booking.eventId, tier.TierName);

                if (availableStock >= tier.ReservedQuantity)
                {
                    // Reserve tickets in Redis for this tier
                    bool tierReserved = await ReserveTickets(booking.eventId, tier.TierName, tier.ReservedQuantity, booking.userEmail);

                    if (tierReserved)
                    {
                        reservedTiers.Add(tier); // Keep track of successfully reserved tiers
                    }
                    else
                    {
                        allReserved = false;
                        failedTiers.Add(tier);
                        break;  // Stop further reservation if any tier fails
                    }
                }
                else
                {
                    allReserved = false;
                    failedTiers.Add(tier);
                    break;  // Stop further reservation if any tier is unavailable
                }
            }

            // If any tier reservation failed, cancel the entire booking process
            if (!allReserved)
            {
                // Rollback all the successful reservations
                foreach (var tier in reservedTiers)
                {
                    CancelTickets(booking.eventId, tier.TierName, booking.userEmail, tier.ReservedQuantity);
                }

                // Handle failed reservation (you can notify the user about unavailable tiers)
                return false;  // Indicate that the booking failed
            }

            // If all tiers are successfully reserved, update booking status
            booking.status = Status.Reserved;
            booking.ReservedTime = DateTime.UtcNow;

            // Save the booking to the database or cache
            // _bookingService.Save(booking);

            return true;
        }

        public async Task<bool> ConfirmTicket(BookTicketDTO dto)
        {
            foreach (var item in dto.reservationData)
            {
                string key = $"event:{dto.eventId}:user:{dto.userEmail}:tier:{item.TierName}:reserved";
                bool resultDeletion = await _db.KeyDeleteAsync(key);
            }
            dto.status = Status.Confirmed;
            var entity = _mapper.Map<BookTicketDTO, Booking>(dto);
            Booking result = await _repository.UpdateBooking(entity);
            return result != null ? true : false;

        }

        public async Task CancelTickets(int eventId, string tierName, string userEmail, int reservedQuantity)
        {
            string key = $"event:{eventId}:user:{userEmail}:tier:{tierName}:reserved";
            string stockKey = $"event:{eventId}:tier:{tierName}:stock";
            int value = 0;
            int.TryParse(await _db.StringGetAsync(stockKey), out value);
            if (value <= 0)
            {
                return;
            }
            int tierReservedQuantity;
            int.TryParse(await _db.StringGetAsync(key), out tierReservedQuantity);
            if (tierReservedQuantity > 0)
            {
                long incrementedStock = await _db.StringIncrementAsync(stockKey, tierReservedQuantity);
            }
        }

        public async Task<bool> ReserveTickets(int eventId, string tierName, int reservedQuantity, string userEmail)
        {
            string key = $"event:{eventId}:user:{userEmail}:tier:{tierName}:reserved";
            string stockKey = $"event:{eventId}:tier:{tierName}:stock";
            int value = 0;
            int.TryParse(await _db.StringGetAsync(stockKey), out value);
            if (value <= 0)
            {
                return false;
            }
            bool result = await _db.StringSetAsync(key, reservedQuantity, TimeSpan.FromMinutes(5));
            long decrementResult = await _db.StringDecrementAsync(stockKey, reservedQuantity);
            bool correctDecrment = decrementResult < value;
            return correctDecrment && result;

        }




        public async Task CancelBooking(BookTicketDTO dto)
        {


            foreach (var item in dto.reservationData)
            {
                int value;
                int currentstock;
                string key = $"event:{dto.eventId}:user:{dto.userEmail}:tier:{item.TierName}:reserved";
                string stockKey = $"event:{dto.eventId}:tier:{item.TierName}:stock";

                int.TryParse(await _db.StringGetAsync(stockKey), out currentstock);

                int.TryParse(await _db.StringGetAsync(key), out value);
                long incrementedStock = await _db.StringIncrementAsync(stockKey, value);
                if (currentstock < incrementedStock)
                {
                    bool resultDeletion = await _db.KeyDeleteAsync(key);
                }

            }

            dto.status = Status.Failed;
            var entity = _mapper.Map<BookTicketDTO, Booking>(dto);
            await _repository.UpdateBooking(entity);

        }
    }
}

