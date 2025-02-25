using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class ExpenseFactory
    {
        public static ExpenseDTO ToDTO(ExpenseEntity expense)
        {
            return new ExpenseDTO
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                ProjectId = expense.ProjectId
            };
        }

        public static ExpenseEntity ToEntity(ExpenseDTO expenseDTO)
        {
            return new ExpenseEntity
            {
                Id = expenseDTO.Id,
                Description = expenseDTO.Description,
                Amount = expenseDTO.Amount,
                Date = expenseDTO.Date,
                ProjectId = expenseDTO.ProjectId
            };
        }
    }
}
