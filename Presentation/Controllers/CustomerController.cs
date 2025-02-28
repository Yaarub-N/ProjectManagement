using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController(CustomerService customerService) : ControllerBase
    {
        private readonly CustomerService _customerService = customerService;

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            var response = await _customerService.CreateCustomerAsync(customerDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response = await _customerService.GetAllCustomersAsync();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer(int customerId)
        {
            var response = await _customerService.GetCustomerByIdAsync(customerId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] CustomerDTO customerDTO)
        {
            var response = await _customerService.UpdateCustomerAsync(customerId, customerDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            var response = await _customerService.DeleteCustomerAsync(customerId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}
