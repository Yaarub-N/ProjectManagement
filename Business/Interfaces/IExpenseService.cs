using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IExpenseService
    {
        Task<ServiceResponse<ExpenseDTO>> CreateExpenseAsync(ExpenseDTO expenseDTO);
        Task<ServiceResponse<bool>> DeleteExpenseAsync(int expenseId);
        Task<ServiceResponse<ExpenseDTO>> GetExpenseByIdAsync(int expenseId);
        Task<ServiceResponse<ExpenseDTO>> UpdateExpenseAsync(int expenseId, ExpenseDTO expenseDTO);
    }
}