using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TicketingService.DTOs
{
    public class TicketDTO
    {
        [JsonIgnore]
        public int EventTicketInventoryId { get; set; }
        public int eventId { get; set; }

        public string eventName { get; set; }

        public string eventDescription { get; set; }

        public string ticketTier { get; set; }
        public decimal pricePerTicket { get; set; }

        public int total { get; set; }

        public int available { get; set; }
    }
}
