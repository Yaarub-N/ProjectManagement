using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class ProjectEmployeeFactory
    {
        public static ProjectEmployeeDTO ToDTO(ProjectEmployeeEntity projectEmployee)
        {
            return new ProjectEmployeeDTO
            {
                ProjectId = projectEmployee.ProjectId,
                EmployeeId = projectEmployee.EmployeeId,
                ProjectName = projectEmployee.Project.Name,
                EmployeeName = $"{projectEmployee.Employee.FirstName} {projectEmployee.Employee.LastName}"
            };
        }

        public static ProjectEmployeeEntity ToEntity(ProjectEmployeeDTO projectEmployeeDTO)
        {
            return new ProjectEmployeeEntity
            {
                ProjectId = projectEmployeeDTO.ProjectId,
                EmployeeId = projectEmployeeDTO.EmployeeId
            };
        }

        public static IEnumerable<ProjectEmployeeDTO> ToDTOList(IEnumerable<ProjectEmployeeEntity> projectEmployees)
        {
            return projectEmployees.Select(ToDTO);
        }
    }
}
