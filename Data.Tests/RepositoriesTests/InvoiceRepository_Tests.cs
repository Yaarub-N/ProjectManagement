using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.RepositoriesTests;

public class InvoiceRepository_Tests
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
    public async Task GetAllAsync_ShouldReturnAllInvoices()
    {
        // Arrange
        var context = GetDataContext();
        context.Invoices.AddRange(TestData.InvoiceEntityTestData);
        await context.SaveChangesAsync();

        IInvoiceRepository repository = new InvoiceRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.InvoiceEntityTestData.Length, result.Count());
    }
}
