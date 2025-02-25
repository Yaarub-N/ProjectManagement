using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class ProjectEmployeeService(IProjectEmployeeRepository projectEmployeeRepository) : IProjectEmployeeService
    {
        private readonly IProjectEmployeeRepository _projectEmployeeRepository = projectEmployeeRepository;

        public async Task<ServiceResponse<ProjectEmployeeDTO>> AssignEmployeeToProjectAsync(int projectId, int employeeId)
        {
            try
            {
                if (projectId <= 0 || employeeId <= 0)
                    return new ServiceResponse<ProjectEmployeeDTO>(null!, false, "Invalid project or employee ID.");

                var projectEmployee = new ProjectEmployeeEntity
                {
                    ProjectId = projectId,
                    EmployeeId = employeeId
                };

                var result = await _projectEmployeeRepository.AddAsync(projectEmployee);

                if (!result)
                    return new ServiceResponse<ProjectEmployeeDTO>(null!, false, "Failed to assign employee to project.");

                return new ServiceResponse<ProjectEmployeeDTO>(ProjectEmployeeFactory.ToDTO(projectEmployee), true, "Employee assigned to project successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProjectEmployeeDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> RemoveEmployeeFromProjectAsync(int projectId, int employeeId)
        {
            try
            {
                var projectEmployee = await _projectEmployeeRepository.GetAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);
                if (projectEmployee == null)
                    return new ServiceResponse<bool>(false, false, "Employee not found in the specified project.");

                var result = await _projectEmployeeRepository.RemoveAsync(projectEmployee);
                return result
                    ? new ServiceResponse<bool>(true, true, "Employee removed from project successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to remove employee from project.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProjectEmployeeDTO>>> GetEmployeesByProjectIdAsync(int projectId)
        {
            try
            {
                if (projectId <= 0)
                    return new ServiceResponse<IEnumerable<ProjectEmployeeDTO>>(null!, false, "Invalid project ID.");

                // Use the new method to filter by ProjectId
                var projectEmployees = await _projectEmployeeRepository.GetAllAsyncWithPredicate(pe => pe.ProjectId == projectId);
                if (!projectEmployees.Any())
                    return new ServiceResponse<IEnumerable<ProjectEmployeeDTO>>(null!, false, "No employees found for this project.");

                return new ServiceResponse<IEnumerable<ProjectEmployeeDTO>>(ProjectEmployeeFactory.ToDTOList(projectEmployees), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<IEnumerable<ProjectEmployeeDTO>>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

    }
}
