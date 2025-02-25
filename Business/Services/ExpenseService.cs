using Business.Interfaces;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<ServiceResponse<ExpenseDTO>> CreateExpenseAsync(ExpenseDTO expenseDTO)
        {
            try
            {
                if (expenseDTO == null)
                    return new ServiceResponse<ExpenseDTO>(null!, false, "Invalid expense data.");

                var expenseEntity = ExpenseFactory.ToEntity(expenseDTO);
                var result = await _expenseRepository.AddAsync(expenseEntity);

                if (!result)
                    return new ServiceResponse<ExpenseDTO>(null!, false, "Failed to create expense.");

                return new ServiceResponse<ExpenseDTO>(ExpenseFactory.ToDTO(expenseEntity), true, "Expense created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<ExpenseDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<ExpenseDTO>> GetExpenseByIdAsync(int expenseId)
        {
            try
            {
                var expense = await _expenseRepository.GetAsync(e => e.Id == expenseId);
                if (expense == null)
                    return new ServiceResponse<ExpenseDTO>(null!, false, "Expense not found.");

                return new ServiceResponse<ExpenseDTO>(ExpenseFactory.ToDTO(expense), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<ExpenseDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<ExpenseDTO>> UpdateExpenseAsync(int expenseId, ExpenseDTO expenseDTO)
        {
            try
            {
                if (expenseId <= 0 || expenseDTO == null)
                    return new ServiceResponse<ExpenseDTO>(null!, false, "Invalid expense update request.");

                var existingExpense = await _expenseRepository.GetAsync(e => e.Id == expenseId);
                if (existingExpense == null)
                    return new ServiceResponse<ExpenseDTO>(null!, false, "Expense not found.");

                existingExpense.Description = expenseDTO.Description;
                existingExpense.Amount = expenseDTO.Amount;
                existingExpense.Date = expenseDTO.Date;
                existingExpense.ProjectId = expenseDTO.ProjectId;

                var result = await _expenseRepository.UpdateAsync(existingExpense);
                return result
                    ? new ServiceResponse<ExpenseDTO>(ExpenseFactory.ToDTO(existingExpense), true, "Expense updated successfully.")
                    : new ServiceResponse<ExpenseDTO>(null!, false, "Failed to update expense.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<ExpenseDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteExpenseAsync(int expenseId)
        {
            try
            {
                var existingExpense = await _expenseRepository.GetAsync(e => e.Id == expenseId);
                if (existingExpense == null)
                    return new ServiceResponse<bool>(false, false, "Expense not found.");

                var result = await _expenseRepository.RemoveAsync(existingExpense);
                return result
                    ? new ServiceResponse<bool>(true, true, "Expense deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete expense.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
