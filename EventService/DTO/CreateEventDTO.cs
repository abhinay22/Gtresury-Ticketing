namespace EventService.DTO
{
    public class CreateEventDTO
    {

        public string eventName { get; set; }

        public string eventDescription { get; set; }

        public DateTime startDate { get; set; }


        public DateTime endDate { get; set; }

        public VenueDTO venue { get; set; }

        public PricingTier pricingTier{get;set;}

        public int totalQuantity {  get; set; }
    }

    
}
