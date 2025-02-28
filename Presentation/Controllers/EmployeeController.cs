using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController(EmployeeService employeeService) : ControllerBase
    {
        private readonly EmployeeService _employeeService = employeeService;

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            var response = await _employeeService.CreateEmployeeAsync(employeeDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet("projectManagers")]
        public async Task<IActionResult> GetProjectManagers()
        {
            var response = await _employeeService.GetAllEmployeesAsync();
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployees(int employeeId)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(employeeId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }
   

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] EmployeeDTO employeeDTO)
        {
            var response = await _employeeService.UpdateEmployeeAsync(employeeId, employeeDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var response = await _employeeService.DeleteEmployeeAsync(employeeId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}
