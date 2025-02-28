using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class EmployeeFactory
    {
        public static EmployeeDTO ToDTO(EmployeeEntity employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                RoleId = employee.RoleId,
                RoleName = employee.Role?.RoleName ?? "Unknown Role"
            };
        }

        public static EmployeeEntity ToEntity(EmployeeDTO employeeDTO)
        {
            return new EmployeeEntity
            {
                Id = employeeDTO.Id,
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                Email = employeeDTO.Email,
                PhoneNumber = employeeDTO.PhoneNumber,
                RoleId = employeeDTO.RoleId
            };
        }

        public static IEnumerable<EmployeeDTO> ToDTOList(IEnumerable<EmployeeEntity> customers)
        {
            return customers.Select(ToDTO);
        }
    }
}
