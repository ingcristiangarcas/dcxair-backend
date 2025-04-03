using DCXAir.Domain.Entities;
using DCXAir.Domain.Interfaces;
using DCXAir.Domain.Models;
using System.Text.Json;


namespace DCXAir.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly List<Flight> _flights = new List<Flight>();

        public FlightRepository(string jsonFilePath)
        {
            try
            {
                // Asegurarse de que el archivo existe
                if (!File.Exists(jsonFilePath))
                {
                    throw new FileNotFoundException($"El archivo JSON no se encontró en la ruta: {jsonFilePath}");
                }

                // Leer el archivo JSON y deserializarlo
                var json = File.ReadAllText(jsonFilePath);
                var marketData = JsonSerializer.Deserialize<List<MarketData>>(json);

                // Convertir MarketData a Flight
                if (marketData != null)
                {
                    foreach (var market in marketData)
                    {
                        _flights.Add(new Flight
                        {
                            Id = Guid.NewGuid().ToString(),
                            Origin = market.Origin,
                            Destination = market.Destination,
                            Price = market.Price,
                            Currency = market.Currency
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                
                // Inicializar con una lista vacía para evitar NullReferenceException
                _flights = new List<Flight>();
            }
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await Task.FromResult(_flights);
        }

        public async Task<IEnumerable<Flight>> GetFlightsByOriginAndDestinationAsync(string origin, string destination)
        {
            return await Task.FromResult(_flights.Where(f => 
                f.Origin.Equals(origin, StringComparison.OrdinalIgnoreCase) && 
                f.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase)));
        }
    }
}