using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IProjectEmployeeService
    {
        Task<ServiceResponse<ProjectEmployeeDTO>> AssignEmployeeToProjectAsync(int projectId, int employeeId);
        Task<ServiceResponse<IEnumerable<ProjectEmployeeDTO>>> GetEmployeesByProjectIdAsync(int projectId);
        Task<ServiceResponse<bool>> RemoveEmployeeFromProjectAsync(int projectId, int employeeId);
    }
}