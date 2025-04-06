using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingService.DTOs
{
    public class PaymentDTO
    {
        public string PaymentStatus { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
