using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/dateranges")]
    public class DateRangeController : ControllerBase
    {
        private readonly DateRangeService _dateRangeService;

        public DateRangeController(DateRangeService dateRangeService)
        {
            _dateRangeService = dateRangeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDateRange([FromBody] DateRangeDTO dateRangeDTO)
        {
            var response = await _dateRangeService.CreateDateRangeAsync(dateRangeDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{dateRangeId}")]
        public async Task<IActionResult> GetDateRange(int dateRangeId)
        {
            var response = await _dateRangeService.GetDateRangeByIdAsync(dateRangeId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{dateRangeId}")]
        public async Task<IActionResult> UpdateDateRange(int dateRangeId, [FromBody] DateRangeDTO dateRangeDTO)
        {
            var response = await _dateRangeService.UpdateDateRangeAsync(dateRangeId, dateRangeDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{dateRangeId}")]
        public async Task<IActionResult> DeleteDateRange(int dateRangeId)
        {
            var response = await _dateRangeService.DeleteDateRangeAsync(dateRangeId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}
