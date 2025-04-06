using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ticketing.Core;
using Ticketing.Core.Entities;
using Ticketing_.Infrastructure.DBContext;
using Ticketing_.Infrastructure.Migrations;

namespace Ticketing_.Infrastructure
{
    public class TicketingRepository : ITicketingRepository
    {
        private readonly TicketingDBContext _context;


        public TicketingRepository(TicketingDBContext context)
        {
            _context = context;
        }
        public async Task CreateEventTicketRepository(IEnumerable<EventTicketInventory> inventory)
        {
            _context.ticketingInventory.AddRange(inventory);
            await _context.SaveChangesAsync();

        }

        public async Task CreateReservation(Booking book)
        {
            _context.booking.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking> GetBooking(int id)
        {
            return await _context.booking.Include(x => x.reservationData)
                 .FirstOrDefaultAsync(x => x.BookingId == id);
        }
        public async Task<List<EventTicketInventory>> GetAllEvents(int eventId)
        {
            return await _context.ticketingInventory.Where(x => x.eventId == eventId).ToListAsync();
        }

        async Task<Booking> ITicketingRepository.GetBooking(int bookingId)
        {
            return await _context.booking.AsNoTracking().Include(x => x.reservationData).AsNoTracking().
                FirstOrDefaultAsync(x => x.BookingId == bookingId);
        }

        public async Task<Booking> UpdateBooking(Booking book)
        {

            var inventoryList = await _context.ticketingInventory
             .Where(i => i.eventId == book.eventId)
               .ToListAsync();

            decimal bookingtotalAmount = 0;

            //if book is confirmed adjust the inventory

            if (book.status.ToUpper() == "CONFIRMED")
            {
                foreach (var reservation in book.reservationData)
                {
                    var matchingInventory = inventoryList
                        .FirstOrDefault(i => i.ticketTier == reservation.TierName);

                    if (matchingInventory != null)
                    {
                        int reservedQty = int.Parse(reservation.ReservedQuantity); // Or use int directly if already typed

                        if (matchingInventory.available >= reservedQty)
                        {
                            bookingtotalAmount += reservedQty * matchingInventory.pricePerTicket;
                            reservation.TotalPrice = reservedQty * matchingInventory.pricePerTicket;
                            matchingInventory.available -= reservedQty;
                            _context.ticketingInventory.Update(matchingInventory);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Not enough tickets for tier {reservation.TierName}");
                        }
                    }
                }
            }
            book.totalPrice = bookingtotalAmount;
            _context.booking.Update(book);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return book;
        }



    }
}
