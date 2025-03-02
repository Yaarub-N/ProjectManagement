﻿using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.RepositoriesTests;

public class TimesheetRepository_Tests
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
    public async Task GetAllAsync_ShouldReturnAllTimesheets()
    {
        // Arrange
        var context = GetDataContext();
        context.Timesheets.AddRange(TestData.TimesheetEntityTestData);
        await context.SaveChangesAsync();

        ITimesheetRepository repository = new TimesheetRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.TimesheetEntityTestData.Length, result.Count());
    }
}
