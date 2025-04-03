namespace DCXAir.Domain.Entities
{
    public class Flight
    {
        public string? Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD"; // Moneda predeterminada
        public string FlightCarrier { get; set; } // Aerolínea
        public string FlightNumber { get; set; } // Número de vuelo
    }
}
