using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Core.Entities
{
    public  class Booking
    {
        public int BookingId {  get; set; }
        public int eventId { get; set; }

        public string userEmail { get; set; }

        public Guid transactionRef { get; set; }

        public DateTime ReservedTime { get; set; }

        public DateTime FinalizationTime { get; set; }

        public String status { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<BookingTicketTier> reservationData { get; set; }

        public decimal totalPrice { get;set; }
    }
}
