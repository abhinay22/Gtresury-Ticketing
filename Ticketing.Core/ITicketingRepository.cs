using Ticketing.Core.Entities;

namespace Ticketing.Core
{
    public interface ITicketingRepository
    {
        Task CreateEventTicketRepository(IEnumerable<EventTicketInventory> inventory);
        Task<List<EventTicketInventory>> GetAllEvents(int eventId);

        Task<Booking> UpdateBooking(Booking book);

        Task<Booking> GetBooking(int bookingId);

        Task CreateReservation(Booking book);
    }
}
