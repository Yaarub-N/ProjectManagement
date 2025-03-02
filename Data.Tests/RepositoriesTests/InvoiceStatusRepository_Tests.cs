using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.RepositoriesTests;

public class InvoiceStatusRepository_Tests
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
    public async Task GetAllAsync_ShouldReturnAllInvoiceStatuses()
    {
        // Arrange
        var context = GetDataContext();
        context.InvoiceStatuses.AddRange(TestData.InvoiceStatusEntityTestData);
        await context.SaveChangesAsync();

        IInvoiceStatusRepository repository = new InvoiceStatusRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.InvoiceStatusEntityTestData.Length, result.Count());
    }
}
