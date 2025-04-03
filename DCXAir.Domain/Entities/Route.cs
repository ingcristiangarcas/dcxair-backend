namespace DCXAir.Domain.Entities
{
    public class Route
    {
        public string Id {get; set; }
        public List<Flight> Flights {get; set; } = new List<Flight>();
        public bool IsRoundTrip{get; set; }
        public decimal TotalPrice {get; set; }
        public string Currency {get; set; }
    }
}