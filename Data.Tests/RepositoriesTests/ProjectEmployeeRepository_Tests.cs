using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.RepositoriesTests;

public class ProjectEmployeeRepository_Tests
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
    public async Task GetAllAsync_ShouldReturnAllProjectEmployees()
    {
        // Arrange
        var context = GetDataContext();
        context.ProjectEmployees.AddRange(TestData.ProjectEmployeeEntityTestData);
        await context.SaveChangesAsync();

        IProjectEmployeeRepository repository = new ProjectEmployeeRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.ProjectEmployeeEntityTestData.Length, result.Count());
    }
}
