using DCXAir.Domain.Entities;

namespace DCXAir.Domain.Interfaces
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<IEnumerable<Flight>> GetFlightsByOriginAndDestinationAsync(string origin, string destination);
    }
}