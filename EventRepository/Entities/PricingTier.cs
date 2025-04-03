using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketing.Core.Entities
{
    public class PricingTier
    {
        public int PricingTierId { get; set; }

        public string tierName { get; set; }

        public string price { get; set; }

        public int totalTicket { get; set; }

        public Event Event { get; set; }
    }
}
