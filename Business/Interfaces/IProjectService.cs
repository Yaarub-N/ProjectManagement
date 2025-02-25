using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<ServiceResponse<ProjectDTO>> CreateProjectAsync(ProjectRegistrationForm projectForm);
        Task<ServiceResponse<bool>> DeleteProjectAsync(int projectNumber);
        Task<ServiceResponse<IEnumerable<ProjectDTO>>> GetAllProjectsAsync();
        Task<ServiceResponse<IEnumerable<ProjectDTO>>> GetProjectByIdAsync(int projectNumber);
        Task<ServiceResponse<ProjectDTO>> UpdateProjectAsync(int projectNumber, ProjectRegistrationForm projectForm);
    }
}