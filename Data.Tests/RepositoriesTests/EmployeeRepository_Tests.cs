using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.RepositoriesTests;

public class EmployeeRepository_Tests
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
    public async Task GetAllAsync_ShouldReturnAllEmployees()
    {
        // Arrange
        var context = GetDataContext();
        context.Employees.AddRange(TestData.EmployeeEntityTestData);
        await context.SaveChangesAsync();

        IEmployeeRepository repository = new EmployeeRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.EmployeeEntityTestData.Length, result.Count());
    }
}
