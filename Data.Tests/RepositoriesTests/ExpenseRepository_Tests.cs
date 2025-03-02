using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.RepositoriesTests;

public class ExpenseRepository_Tests
{

    private DataContext GetDataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureCreated();
        return context;
    }
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllExpenses()
    {
        // Arrange
        var context = GetDataContext();
        context.Expenses.AddRange(TestData.ExpenseEntityTestData);
        await context.SaveChangesAsync();

        IExpenseRepository repository = new ExpenseRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.ExpenseEntityTestData.Length, result.Count());
    }
}
