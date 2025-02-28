using Business.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

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
        public async Task<IActionResult> CreateProject([FromBody] ProjectRegistrationForm form)
        {
            if (form == null)
                return BadRequest("Invalid project data.");

            var response = await _projectService.CreateProjectAsync(form);
            return response.Success
                ? CreatedAtAction(nameof(GetProject), new { projectNumber = response.Data!.ProjectNumber }, response.Data)
                : BadRequest(response.Message);
        }

        [HttpPut("{projectNumber}")]
        public async Task<IActionResult> UpdateProject(int projectNumber, [FromBody] ProjectRegistrationForm form)
        {
            if (form == null)
                return BadRequest("Invalid project update request.");

            var response = await _projectService.UpdateProjectAsync(projectNumber, form);
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
