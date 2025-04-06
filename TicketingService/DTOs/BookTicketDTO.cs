using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingService.DTOs
{
    public class BookTicketDTO
    {
        public int BookingId { get; set; }
        public int eventId { get; set; }

        public string userEmail { get; set; }

        public Guid transactionRef { get; set; }

        public DateTime ReservedTime { get; set; }

        public DateTime FinalizationTime { get; set; }

        public Status status { get; set; }

        public string Notes { get;set; }

        public decimal totalPrice { get; set; }

        public List<TicketTierReservation> reservationData { get; set; }
    }

    public class TicketTierReservation
    {
        public int BookingTicketTierId { get; set; }
        public string TierName { get; set; }
        public int ReservedQuantity { get; set; }
        
    }

    public enum Status
    {
        Confirmed,
        Reserved,
        Failed
    }
}
