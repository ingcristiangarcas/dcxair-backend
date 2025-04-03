using DCXAir.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DCXAir.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(DCXAirDbContext context)
        {
            // Aplicar migraciones
            context.Database.Migrate();

            // Si ya hay vuelos, no hacer nada
            if (context.Flights.Any()) return;

            // Cargar `markets.json`
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "markets.json");
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("⚠️ Archivo markets.json no encontrado.");
                return;
            }

            var jsonData = File.ReadAllText(jsonFilePath);
            var flights = JsonSerializer.Deserialize<List<FlightJsonModel>>(jsonData);

            if (flights == null || flights.Count == 0)
            {
                Console.WriteLine("⚠️ No se encontraron vuelos en el archivo JSON.");
                return;
            }

            // Convertir `FlightJsonModel` a `Flight` y guardar en la base de datos
            var flightEntities = flights
    .Where(f => f.Price > 0) // Evita precios negativos o cero
    .Select(f => new Flight
    {
        Origin = f.Origin,
        Destination = f.Destination,
        Price = f.Price,
        Currency = "USD",
        FlightCarrier = f.Transport.FlightCarrier,
        FlightNumber = f.Transport.FlightNumber
    }).ToList();


            context.Flights.AddRange(flightEntities);
            context.SaveChanges();
            Console.WriteLine($"✅ {flightEntities.Count} vuelos cargados desde markets.json.");
        }
    }

    // Modelo para mapear el JSON
    public class FlightJsonModel
{
    public string Origin { get; set; } = string.Empty; 
    public string Destination { get; set; } = string.Empty; 
    public decimal Price { get; set; } 
    public TransportInfo Transport { get; set; } = new TransportInfo();
}

public class TransportInfo
{
    public string FlightCarrier { get; set; } = string.Empty; 
    public string FlightNumber { get; set; } = string.Empty; 
}

}
