using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/projectemployees")]
    public class ProjectEmployeeController(ProjectEmployeeService projectEmployeeService) : ControllerBase
    {
        private readonly ProjectEmployeeService _projectEmployeeService = projectEmployeeService;

        [HttpPost("assign")]
        public async Task<IActionResult> AssignEmployeeToProject([FromQuery] int projectId, [FromQuery] int employeeId)
        {
            var response = await _projectEmployeeService.AssignEmployeeToProjectAsync(projectId, employeeId);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveEmployeeFromProject([FromQuery] int projectId, [FromQuery] int employeeId)
        {
            var response = await _projectEmployeeService.RemoveEmployeeFromProjectAsync(projectId, employeeId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetEmployeesByProjectId(int projectId)
        {
            var response = await _projectEmployeeService.GetEmployeesByProjectIdAsync(projectId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }
    }
}