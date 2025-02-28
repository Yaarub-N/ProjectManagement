using Domain.DTO;
using Domain.ServiceResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<ServiceResponse<ProjectDTO>> CreateProjectAsync(ProjectRegistrationForm form);
        Task<ServiceResponse<bool>> DeleteProjectAsync(int projectNumber);
        Task<ServiceResponse<IEnumerable<ProjectDTO>>> GetAllProjectsAsync();
        Task<ServiceResponse<ProjectDTO>> GetProjectByIdAsync(int projectNumber);
        Task<ServiceResponse<ProjectDTO>> UpdateProjectAsync(int projectNumber, ProjectRegistrationForm form);
    }
}
