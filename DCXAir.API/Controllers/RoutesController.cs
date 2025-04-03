using DCXAir.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace DCXAir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet("oneway")]
public async Task<IActionResult> GetOneWayRoutes(string origin, string destination, string currency = "USD")
{
    try
    {
        if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
        {
            return BadRequest("Origin and destination are required");
        }

        var routes = await _routeService.GetOneWayRoutesAsync(origin, destination, currency);
        
        if (!routes.Any())
        {
            return NotFound("No routes found for the specified criteria");
        }

        return Ok(routes);
    }
    catch (Exception ex)
    {
    
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        
        return StatusCode(500, "An error occurred while processing your request. Please try again later.");
    }
}

        [HttpGet("roundtrip")]
        public async Task<IActionResult> GetRoundTripRoutes(string origin, string destination, string currency = "USD")
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
            {
                return BadRequest("Origin and destination are required");
            }

            var routes = await _routeService.GetRoundTripRoutesAsync(origin, destination, currency);
            
            if (!routes.Any())
            {
                return NotFound("No routes found for the specified criteria");
            }

            return Ok(routes);
        }
    }
}