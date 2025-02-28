using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationController(LocationService locationService) : ControllerBase
    {
        private readonly LocationService _locationService = locationService;

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] LocationDTO locationDTO)
        {
            var response = await _locationService.CreateLocationAsync(locationDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var response = await _locationService.GetAllLocationsAsync();
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpGet("{locationId}")]
        public async Task<IActionResult> GetLocation(int locationId)
        {
            var response = await _locationService.GetLocationByIdAsync(locationId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{locationId}")]
        public async Task<IActionResult> UpdateLocation(int locationId, [FromBody] LocationDTO locationDTO)
        {
            var response = await _locationService.UpdateLocationAsync(locationId, locationDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{locationId}")]
        public async Task<IActionResult> DeleteLocation(int locationId)
        {
            var response = await _locationService.DeleteLocationAsync(locationId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}
