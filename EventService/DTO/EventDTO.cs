using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventService.DTO
{
    public class EventDTO
    { 
        public int eventId { get; set; }
        public string eventName { get; set; }

        public string eventDescription { get; set; }

        public DateTime startDate { get; set; }


        public DateTime endDate { get; set; }

        public VenueDTO venue { get; set; }

        public List<PricingTierDTO> pricingTier { get; set; }

        public int totalQuantity { get; set; }
    }
}
