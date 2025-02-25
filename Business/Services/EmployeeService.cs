using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ServiceResponse<EmployeeDTO>> CreateEmployeeAsync(EmployeeDTO employeeDTO)
        {
            try
            {
                if (employeeDTO == null)
                    return new ServiceResponse<EmployeeDTO>(null!, false, "Invalid employee data.");

                var employeeEntity = EmployeeFactory.ToEntity(employeeDTO);
                var result = await _employeeRepository.AddAsync(employeeEntity);

                if (!result)
                    return new ServiceResponse<EmployeeDTO>(null!, false, "Failed to create employee.");

                return new ServiceResponse<EmployeeDTO>(EmployeeFactory.ToDTO(employeeEntity), true, "Employee created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<EmployeeDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<EmployeeDTO>> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetAsync(e => e.Id == employeeId);
                if (employee == null)
                    return new ServiceResponse<EmployeeDTO>(null!, false, "Employee not found.");

                return new ServiceResponse<EmployeeDTO>(EmployeeFactory.ToDTO(employee), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<EmployeeDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<EmployeeDTO>> UpdateEmployeeAsync(int employeeId, EmployeeDTO employeeDTO)
        {
            try
            {
                if (employeeId <= 0 || employeeDTO == null)
                    return new ServiceResponse<EmployeeDTO>(null!, false, "Invalid employee update request.");

                var existingEmployee = await _employeeRepository.GetAsync(e => e.Id == employeeId);
                if (existingEmployee == null)
                    return new ServiceResponse<EmployeeDTO>(null!, false, "Employee not found.");

                existingEmployee.FirstName = employeeDTO.FirstName;
                existingEmployee.LastName = employeeDTO.LastName;
                existingEmployee.Email = employeeDTO.Email;
                existingEmployee.PhoneNumber = employeeDTO.PhoneNumber;
                existingEmployee.RoleId = employeeDTO.RoleId;

                var result = await _employeeRepository.UpdateAsync(existingEmployee);
                return result
                    ? new ServiceResponse<EmployeeDTO>(EmployeeFactory.ToDTO(existingEmployee), true, "Employee updated successfully.")
                    : new ServiceResponse<EmployeeDTO>(null!, false, "Failed to update employee.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<EmployeeDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                var existingEmployee = await _employeeRepository.GetAsync(e => e.Id == employeeId);
                if (existingEmployee == null)
                    return new ServiceResponse<bool>(false, false, "Employee not found.");

                var result = await _employeeRepository.RemoveAsync(existingEmployee);
                return result
                    ? new ServiceResponse<bool>(true, true, "Employee deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete employee.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
