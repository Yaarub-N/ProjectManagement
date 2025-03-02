using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.RepositoriesTests;
public class CustomerRepository_Tests
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
    public async Task GetAllAsync_Should_ReturnAllAdresses()
    {
        // Arrange
        var context = GetDataContext();
        context.Addresses.AddRange(TestData.AdressTestData);
        context.Locations.AddRange(TestData.LocationEntityTestData);
        await context.SaveChangesAsync();

        IAddressRepository repository = new AddressRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.AdressTestData.Length, result.Count());
    }
}

    