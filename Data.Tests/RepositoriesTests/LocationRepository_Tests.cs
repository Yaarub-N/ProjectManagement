using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.RepositoriesTests;

public class LocationRepository_Tests
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
    public async Task GetAllAsync_ShouldReturnAllLocations()
    {
        // Arrange
        var context = GetDataContext();
        context.Locations.AddRange(TestData.LocationEntityTestData);
        await context.SaveChangesAsync();

        ILocationRepository repository = new LocationRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.LocationEntityTestData.Length, result.Count());
    }
}
