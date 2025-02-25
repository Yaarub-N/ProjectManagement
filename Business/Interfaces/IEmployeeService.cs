using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<EmployeeDTO>> CreateEmployeeAsync(EmployeeDTO employeeDTO);
        Task<ServiceResponse<bool>> DeleteEmployeeAsync(int employeeId);
        Task<ServiceResponse<EmployeeDTO>> GetEmployeeByIdAsync(int employeeId);
        Task<ServiceResponse<EmployeeDTO>> UpdateEmployeeAsync(int employeeId, EmployeeDTO employeeDTO);
    }
}