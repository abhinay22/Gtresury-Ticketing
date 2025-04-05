namespace EventContract
{
    public class EventActivated
    {
        public int EventId { get; set; }

        public string eventName { get; set; }

        public string eventDescription { get; set; }

        public DateTime startDate { get; set; }

        public bool isÀctive { get; set; }

        public DateTime endDate { get; set; }

        public Venue venue { get; set; }

        public  List <PricingTier> pricingTier { get; set; }

        public int totalQuantity { get; set; }

    }
}
