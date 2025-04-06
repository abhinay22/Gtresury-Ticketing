using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingService.DTOs
{
    public class CreateBookingDTO
    {
        public int eventId { get; set; }

        public List<PricingTier> tickets { get; set; }

         public string userEmail { get; set; }

        public DateTime Created { get; set; }
    }

    public class PricingTier
    {
        public string tierName {  get; set; }

        public string quantity {  get; set; }

       

    }
}
