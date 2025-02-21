using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController(ProjectService projectService) : ControllerBase
    {
        private readonly ProjectService _projectService = projectService;

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var response = await _projectService.GetAllProjectsAsync();
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpGet("{projectNumber}")]
        public async Task<IActionResult> GetProject(int projectNumber)
        {
            var response = await _projectService.GetProjectByIdAsync(projectNumber);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRegistrationForm projectForm)
        {
            var response = await _projectService.CreateProjectAsync(projectForm);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpPut("{projectNumber}")]
        public async Task<IActionResult> UpdateProject(int projectNumber, [FromBody] ProjectRegistrationForm projectForm)
        {
            var response = await _projectService.UpdateProjectAsync(projectNumber, projectForm);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{projectNumber}")]
        public async Task<IActionResult> DeleteProject(int projectNumber)
        {
            var response = await _projectService.DeleteProjectAsync(projectNumber);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}