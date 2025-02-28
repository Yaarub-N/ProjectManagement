using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/timesheets")]
    public class TimesheetController(TimesheetService timesheetService) : ControllerBase
    {
        private readonly TimesheetService _timesheetService = timesheetService;

        [HttpPost]
        public async Task<IActionResult> CreateTimesheet([FromBody] TimesheetDTO timesheetDTO)
        {
            var response = await _timesheetService.CreateTimesheetAsync(timesheetDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{timesheetId}")]
        public async Task<IActionResult> GetTimesheet(int timesheetId)
        {
            var response = await _timesheetService.GetTimesheetByIdAsync(timesheetId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{timesheetId}")]
        public async Task<IActionResult> UpdateTimesheet(int timesheetId, [FromBody] TimesheetDTO timesheetDTO)
        {
            var response = await _timesheetService.UpdateTimesheetAsync(timesheetId, timesheetDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{timesheetId}")]
        public async Task<IActionResult> DeleteTimesheet(int timesheetId)
        {
            var response = await _timesheetService.DeleteTimesheetAsync(timesheetId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}