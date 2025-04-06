using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingService.DTOs;


namespace Ticketing.Core
{
    public interface ITicketingService
    {
        Task<List<TicketDTO>> GetAllTickets(int eventId);

        Task<bool> ReserveTicketsForUser(BookTicketDTO booking);

        Task<int> CreateReservation(BookTicketDTO booking);

        Task<bool> ConfirmTicket(BookTicketDTO dto);

        Task CancelTickets(int eventId, string tierName, string userEmail, int reservedQuantity);

        public Task CancelBooking(BookTicketDTO dto);
        Task<BookTicketDTO> GetBooking(int id);
    }
}
