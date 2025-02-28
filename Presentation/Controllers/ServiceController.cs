using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServiceController(ServiceService serviceService) : ControllerBase
    {
        private readonly ServiceService _serviceService = serviceService;

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceDTO serviceDTO)
        {
            var response = await _serviceService.CreateServiceAsync(serviceDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var response = await _serviceService.GetAllServicesAsync();
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpGet("{serviceId}")]
        public async Task<IActionResult> GetService(int serviceId)
        {
            var response = await _serviceService.GetServiceByIdAsync(serviceId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{serviceId}")]
        public async Task<IActionResult> UpdateService(int serviceId, [FromBody] ServiceDTO serviceDTO)
        {
            var response = await _serviceService.UpdateServiceAsync(serviceId, serviceDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{serviceId}")]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            var response = await _serviceService.DeleteServiceAsync(serviceId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}
