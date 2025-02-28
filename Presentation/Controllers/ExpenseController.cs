using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public class ExpenseController(ExpenseService expenseService) : ControllerBase
    {
        private readonly ExpenseService _expenseService = expenseService;

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] ExpenseDTO expenseDTO)
        {
            var response = await _expenseService.CreateExpenseAsync(expenseDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{expenseId}")]
        public async Task<IActionResult> GetExpense(int expenseId)
        {
            var response = await _expenseService.GetExpenseByIdAsync(expenseId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{expenseId}")]
        public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] ExpenseDTO expenseDTO)
        {
            var response = await _expenseService.UpdateExpenseAsync(expenseId, expenseDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{expenseId}")]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            var response = await _expenseService.DeleteExpenseAsync(expenseId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}
