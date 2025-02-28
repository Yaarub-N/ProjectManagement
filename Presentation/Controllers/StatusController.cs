using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/statuses")]
    public class StatusController : ControllerBase
    {
        private readonly StatusService _statusService;

        public StatusController(StatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] StatusDTO statusDTO)
        {
            var response = await _statusService.CreateStatusAsync(statusDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStatuses()
        {
            var response = await _statusService.GetAllStatusesAsync();
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpGet("{statusId}")]
        public async Task<IActionResult> GetStatus(int statusId)
        {
            var response = await _statusService.GetStatusByIdAsync(statusId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{statusId}")]
        public async Task<IActionResult> UpdateStatus(int statusId, [FromBody] StatusDTO statusDTO)
        {
            var response = await _statusService.UpdateStatusAsync(statusId, statusDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{statusId}")]
        public async Task<IActionResult> DeleteStatus(int statusId)
        {
            var response = await _statusService.DeleteStatusAsync(statusId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}