using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventContract
{
    public class PricingTier
    {
        public int PricingTierId { get; set; }

        public string tierName { get; set; }

        public decimal price { get; set; }

        public int totalTicket { get; set; }

    }
}
