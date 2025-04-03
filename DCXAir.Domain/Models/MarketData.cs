namespace DCXAir.Domain.Models
{
    public class MarketData
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        
    }
}