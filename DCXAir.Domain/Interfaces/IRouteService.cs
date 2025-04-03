using DCXAir.Domain.Entities;


namespace DCXAir.Domain.Interfaces
{
    public interface IRouteService
    {
        Task<IEnumerable<Route>> GetOneWayRoutesAsync(string origin, string destination, string currency);
        Task<IEnumerable<Route>> GetRoundTripRoutesAsync(string origin, string destination, string currency);
    }
}