using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Tests.RepositoriesTests;

public class ProjectRepositoryTests
{

    private static DataContext GetDataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureCreated();
        return context;
    }



    [Fact]

    public async Task RemoveAsync__ShouldRemove_ProjectAndReturnTrue()
    {
        // Arrange
        var context = GetDataContext();
        context.Projects.AddRange(TestData.ProjectEntityTestData);

        await context.SaveChangesAsync();

        ProjectRepository projectRepository = new(context);
        var projectToRemove = TestData.ProjectEntityTestData[0]; // Kontrollera att detta index är giltigt

        projectToRemove = await context.Projects.FindAsync(projectToRemove.ProjectNumber);

        // Act
        var result = await ((IProjectRepository)projectRepository).RemoveAsync(projectToRemove!);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public async Task UpdateAsync_Should_UpdateProjectAndReturnTrue()
    {
        // Arrange
        var context = GetDataContext();
        context.Projects.AddRange(TestData.ProjectEntityTestData);

        await context.SaveChangesAsync();

        IProjectRepository repository = new ProjectRepository(context);
        var projectUpdate = TestData.ProjectEntityTestData[0]; 

      
        projectUpdate.Name = "Updated Project Name";

        // Act
        var result = await repository.UpdateAsync(projectUpdate);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public async Task GetProjectListAsync__ShouldReturn_ProjectsEntities()
    {
        // Arrange
        var context = GetDataContext();

        // Skapa testdata
        context.Projects.AddRange(TestData.ProjectEntityTestData);
        context.Projects.AddRange(TestData.ProjectEntityTestData);
        context.Projects.AddRange(TestData.ProjectEntityTestData);

        await context.SaveChangesAsync();

        IProjectRepository repository = new ProjectRepository(context);

        // Act
        var result = await repository.GetProjectListAsync();

        // Assert
        Assert.NotNull(result);
    }
    [Fact]

    public async Task GetProjectDetailsAsync__Should_ReturnProject()
    {
        // Arrange
        var context = GetDataContext();

       //Copilot 

        // Add test data
        var project = new ProjectEntity
        {
            ProjectNumber = 101,
            Name = "Test Project",
            TotalPrice = 50000m,
            Status = new StatusEntity { Name = "Active" },
            Customer = new CustomerEntity
            {
                Profile = new ProfileEntity
                {
                    Name = "Test Customer",
                    LastName = "Customer",
                    ContactEmail = "test.customer@example.com",
                    PhoneNumber = "0701122334"
                }
            },
            Service = new ServiceEntity { Name = "Web Development", Description = "Developing websites", HourlyRate = 100m },
            DateRange = new DateRangeEntity { StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) },
        };


        //

        context.Projects.Add(project);
        await context.SaveChangesAsync();

        var repository = new ProjectRepository(context);

        // Act
        var result = await repository.GetProjectDetailsAsync(101);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]

    public async Task AddAsync__Should_AddAProject_AndReturn_Project()
    {

        var context = GetDataContext();

        context.Projects.AddRange(TestData.ProjectEntityTestData);
        await context.SaveChangesAsync();

        IProjectRepository repository = new ProjectRepository(context);

        //chat Gpt4o
        var projectEntity = new ProjectEntity
        {
            ProjectNumber = 10,
            Name = "Project 1",
            Description = "test",
            TotalPrice = 30000m,
            DateRangeId = TestData.DateRangeEntityTestData[0].Id,
            StatusId = TestData.StatusEntityTestData[0].Id,
            ServiceId = TestData.ServiceEntityTestData[0].Id,
        };
        //

        // Act
        var result = await repository.AddAsync(projectEntity);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public async Task ExsistAsync_ShouldReturnFalseWhenProjectDoesNotExist()
    {
        // Arrange
        var context = GetDataContext();
        context.Projects.AddRange(TestData.ProjectEntityTestData);
        await context.SaveChangesAsync();

        IProjectRepository repository = new ProjectRepository(context);


        //chat Gpt4o
        Expression<Func<ProjectEntity, bool>> predicate = p => p.Name == "Test";
        //

        // Act
        var result = await repository.ExsistAsync(predicate);

        // Assert
        Assert.False(result);
    }



    //[Fact]
    //public async Task AddAsync__ShouldReturnAddProjectAndReturnProject()
    //{
    //    // Arrange
    //    var context = GetDataContext();
    //    context.Projects.AddRange(TestData.ProjectEntityTestData);
    //    context.Customers.AddRange(TestData.CustomerEntityTestData); ;
    //    context.DateRanges.AddRange(TestData.DateRangeEntityTestData);
    //    context.Services.AddRange(TestData.ServiceEntityTestData);
    //    context.Employees.AddRange(TestData.EmployeeEntityTestData);
    //    context.Statuses.AddRange(TestData.StatusEntityTestData);

    //    await context.SaveChangesAsync();

    //    IProjectRepository repository = new ProjectRepository(context);

    //    var projectEntity = TestData.ProjectEntityTestData[1];




    //    // Act
    //    var result = await repository.AddAsync(projectEntity);

    //    // Assert
    //    Assert.True(result);
    //}






    //Copilot

    //Your test works because the in-memory database does not enforce strict validation for required
    //properties,
    //and your repository method doesn't validate the entity before saving it. While this behavior
    //is convenient for quick testing,
    //it may lead to missed issues in production. Adding validation
    //logic or testing against a real database will ensure stricter adherence to the model's constraints.

    [Fact]
    public async Task AddAsync__ShouldReturnAddProjectAndReturnProject()
    {
        // Arrange
        var context = GetDataContext();


        context.Projects.AddRange(TestData.ProjectEntityTestData);
        await context.SaveChangesAsync();

        ProjectRepository projectRepository = new(context);

        var projectEntity = new ProjectEntity
        {
            Name = "New Project",
        };

        // Act
        var result = await ((IProjectRepository)projectRepository).AddAsync(projectEntity);

        // Assert
        Assert.True(result);
    }


}