using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicketing.Core.Entities
{
    public class Event
    {
        public int EventId { get; set; }

        public string eventName { get; set; }

        public string eventDescription { get; set; }

        public DateTime startDate { get; set; }

        public bool isÀctive { get; set; }

        public DateTime endDate { get; set; }

        public Venue venue { get; set; }

        public virtual ICollection<PricingTier> pricingTier { get; set; }

        public int totalQuantity { get; set; }
    }
}
