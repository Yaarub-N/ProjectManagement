using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.RepositoriesTests;

public class RoleRepository_Tests
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
    public async Task GetAllAsync_ShouldReturnAllRoles()
    {
        // Arrange
        var context = GetDataContext();
        context.Roles.AddRange(TestData.RoleEntityTestData);
        await context.SaveChangesAsync();

        IRoleRepository repository = new RoleRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.RoleEntityTestData.Length, result.Count());
    }
}
