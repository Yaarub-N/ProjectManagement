

using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.RepositoriesTests;


public class DateRangeRepository_Tests
{
    private static DataContext GetDataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName:Guid.NewGuid().ToString())
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllDateRanges()
    {
        // Arrange
        var context = GetDataContext();
        context.DateRanges.AddRange(TestData.DateRangeEntityTestData);
        await context.SaveChangesAsync();

        DateRangeRepository dateRangeRepository = new(context);

        // Act
        var result = await ((IDateRangeRepository)dateRangeRepository).GetAllAsync();

        // Assert
        Assert.Equal(TestData.DateRangeEntityTestData.Length, result.Count());
    }

}