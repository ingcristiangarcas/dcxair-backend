using DCXAir.Domain.Entities;
using DCXAir.Domain.Interfaces;

namespace DCXAir.Application.Services
{
    public class RouteService : IRouteService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly ICurrencyConverter _currencyConverter;

        public RouteService(IFlightRepository flightRepository, ICurrencyConverter currencyConverter)
        {
            _flightRepository = flightRepository;
            _currencyConverter = currencyConverter;
        }

        public async Task<IEnumerable<Route>> GetOneWayRoutesAsync(string origin, string destination, string currency)
{
    var flights = await _flightRepository.GetFlightsByOriginAndDestinationAsync(origin, destination);
    var routes = new List<Route>();

    foreach (var flight in flights)
    {
        // Verificar que flight.Currency no sea null
        var flightCurrency = string.IsNullOrEmpty(flight.Currency) ? "USD" : flight.Currency;
        
        // Verificar que currency no sea null
        var targetCurrency = string.IsNullOrEmpty(currency) ? "USD" : currency;
        
        // Convertir el precio
        var convertedPrice = _currencyConverter.Convert(flight.Price, flightCurrency, targetCurrency);
        
        // Crear una copia del vuelo con el precio convertido
        var convertedFlight = new Flight
        {
            Id = flight.Id,
            Origin = flight.Origin,
            Destination = flight.Destination,
            Price = convertedPrice,  // Precio convertido
            Currency = targetCurrency  // Nueva moneda
        };

        var route = new Route
        {
            Id = Guid.NewGuid().ToString(),
            Flights = new List<Flight> { convertedFlight },  // Usar el vuelo convertido
            IsRoundTrip = false,
            TotalPrice = convertedPrice,
            Currency = targetCurrency
        };

        routes.Add(route);
    }

    return routes;
}
       public async Task<IEnumerable<Route>> GetRoundTripRoutesAsync(string origin, string destination, string currency)
{
    // Obtenemos vuelos de ida
    var outboundFlights = await _flightRepository.GetFlightsByOriginAndDestinationAsync(origin, destination);
    
    // Obtenemos vuelos de vuelta
    var inboundFlights = await _flightRepository.GetFlightsByOriginAndDestinationAsync(destination, origin);
    
    var routes = new List<Route>();

    // Combinamos vuelos de ida y vuelta para crear rutas completas
    foreach (var outbound in outboundFlights)
    {
        foreach (var inbound in inboundFlights)
        {
            // Verificar que las monedas no sean null
            var outboundCurrency = string.IsNullOrEmpty(outbound.Currency) ? "USD" : outbound.Currency;
            var inboundCurrency = string.IsNullOrEmpty(inbound.Currency) ? "USD" : inbound.Currency;
            var targetCurrency = string.IsNullOrEmpty(currency) ? "USD" : currency;
            
            // Convertir los precios
            var convertedOutboundPrice = _currencyConverter.Convert(outbound.Price, outboundCurrency, targetCurrency);
            var convertedInboundPrice = _currencyConverter.Convert(inbound.Price, inboundCurrency, targetCurrency);
            var totalPrice = convertedOutboundPrice + convertedInboundPrice;
            
            // Crear copias de los vuelos con precios convertidos
            var convertedOutbound = new Flight
            {
                Id = outbound.Id,
                Origin = outbound.Origin,
                Destination = outbound.Destination,
                Price = convertedOutboundPrice,
                Currency = targetCurrency
            };
            
            var convertedInbound = new Flight
            {
                Id = inbound.Id,
                Origin = inbound.Origin,
                Destination = inbound.Destination,
                Price = convertedInboundPrice,
                Currency = targetCurrency
            };

            var route = new Route
            {
                Id = Guid.NewGuid().ToString(),
                Flights = new List<Flight> { convertedOutbound, convertedInbound },
                IsRoundTrip = true,
                TotalPrice = totalPrice,
                Currency = targetCurrency
            };

            routes.Add(route);
        }
    }

    return routes;
}
    }
}