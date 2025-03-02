using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.RepositoriesTests;

public class ProfileRepository_Tests
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
    public async Task GetAllAsync_ShouldReturnAllProfiles()
    {
        // Arrange
        var context = GetDataContext();
        context.Profiles.AddRange(TestData.ProfileEntityTestData);
        await context.SaveChangesAsync();

        IProfileRepository repository = new ProfileRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.ProfileEntityTestData.Length, result.Count());
    }
}
