namespace EventService.DTO
{
    public class PricingTierDTO
    {
        public int PricingTierId { get; set; }
        public string tierName { get; set; }

        public decimal price { get; set; }

        public int totalTicket {  get; set; }
    }
}
