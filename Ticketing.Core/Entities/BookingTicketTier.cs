using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Core.Entities
{
    public class BookingTicketTier
    {
        public int BookingTicketTierId { get; set; }

        public string TierName { get; set; }

        public string ReservedQuantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int BookingId { get; set; }


    }

}
