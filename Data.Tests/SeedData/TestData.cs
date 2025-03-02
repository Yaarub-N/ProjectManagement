using Data.Entities;

namespace Data.Tests.SeedData;

public static class TestData
{
    
    public static readonly LocationEntity[] LocationEntityTestData =
    [
        new() {Id=1, City="vega", PostalCode="23657", Country="Sweden"},
        new() {Id=2, City="Gothenburg", PostalCode="54321", Country="Sweden"},
        new() {Id=3, City="Malmö", PostalCode="67890", Country="Sweden"}
    ];

    public static readonly ProfileEntity[] ProfileEntityTestData =
[
new() {Id=1, Name="Alex", LastName="Johnson", ContactEmail="alex.johnson@example.com", PhoneNumber="0701234567"},
new() {Id=2, Name="Daniel", LastName="Timmer", ContactEmail="daniel.timmer@example.com", PhoneNumber="0702345678"},
new() {Id=3, Name="Tommas", LastName="Dahl", ContactEmail="tommas.dahl@example.com", PhoneNumber="0704567890"},
];

    public static readonly AddressEntity[] AdressTestData =
    [
        new() {Id=1, Street="Vardävägen 61", LocationId=1},
        new() {Id=2, Street="Tenurvägen 25", LocationId=2},
        new() {Id=3, Street="Högaledsgatan 41", LocationId=3}
    ];






    public static readonly CustomerEntity[] CustomerEntityTestData =
[
new()
{
    ProfileId=1,
    AddressId=1,
    Projects=
    [
        new() {
            ProjectNumber=1,
            Name="Project A",
            TotalPrice=10000m,
            DateRangeId=1,
            StatusId=1,
            ServiceId=1,
            ProjectManagerId=1
        }
    ]
},
new()
{
    ProfileId=2,
    AddressId=2,
    Projects=
    [
        new() {
            ProjectNumber=2,
            Name="Project B",
            TotalPrice=20000m,
            DateRangeId=2,
            StatusId=2,
            ServiceId=2,
            ProjectManagerId=2
        }
    ]
},
];
    //
    public static readonly DateRangeEntity[] DateRangeEntityTestData =
    {new(){ StartDate = new DateTime(2021, 10, 2),EndDate = new DateTime(2024, 3, 4), Projects = new List<ProjectEntity> {
        new ProjectEntity
        {
            ProjectNumber = 1,
            Name = "Project A",
            TotalPrice = 10000m,
            DateRangeId = 1,
            StatusId = 1,
            ServiceId = 1
        }
    }
},
new()
{
    StartDate = new DateTime(2021, 10, 2),
    EndDate = new DateTime(2024, 3, 4),
    Projects = new List<ProjectEntity>
    {
        new ProjectEntity
        {
            ProjectNumber = 2,
            Name = "Project B",
            TotalPrice = 20000m,
            DateRangeId = 2,
            StatusId = 2,
            ServiceId = 2
        }
    }
},
new()
{
    StartDate = new DateTime(2021, 10, 2),
    EndDate = new DateTime(2024, 3, 4),
    Projects = new List<ProjectEntity>
    {
        new ProjectEntity
        {
            ProjectNumber = 3,
            Name = "Project C",
            TotalPrice = 15000m,
            DateRangeId = 3,
            StatusId = 3,
            ServiceId = 3
        }
    }
}
};
    //fick hjälp av chatGPT4o m

    public static readonly RoleEntity[] RoleEntityTestData =
    [
        new() {Id=1, RoleName="Project Manager"},
        new() {Id=2, RoleName="Developer"},
        new() {Id=3, RoleName="Tester"},
    ];

    public static readonly ServiceEntity[] ServiceEntityTestData =
    [
        new() {Id=1, Name="Web Development", Description="Developing websites", HourlyRate=100m},
        new() {Id=2, Name="Mobile App Development", Description="Building mobile apps", HourlyRate=150m},
        new() {Id=3, Name="Consulting", Description="Business consulting services", HourlyRate=200m}
    ];

    public static readonly StatusEntity[] StatusEntityTestData =
    [
        new() {Id=1, Name="Not Started"},
        new() {Id=2, Name="In Progress"},
        new() {Id=3, Name="Completed"}
    ];

    public static readonly EmployeeEntity[] EmployeeEntityTestData =
    [
        new() {FirstName="Alex", LastName="Johnson", Email="alex.johnson@example.com", PhoneNumber="0701234567", RoleId=1, ProjectEmployees=[new() { ProjectId=1, EmployeeId=1}]},
        new() {FirstName="Daniel", LastName="Timmer", Email="daniel.timmer@example.com", PhoneNumber="0702345678", RoleId=2, ProjectEmployees=[new() { ProjectId=2, EmployeeId=2}]},
        new() {FirstName="Yaarub", LastName="Al-Farsi", Email="yaarub.alfarsi@example.com", PhoneNumber="0703456789", RoleId=3, ProjectEmployees=[new() { ProjectId=3, EmployeeId=3}]},
    ];


    public static readonly ProjectEntity[] ProjectEntityTestData =
    [
new()
{
    ProjectNumber = 101,
    Name = "Project A",
    TotalPrice = 50000m,
    Status = new StatusEntity { Name = "Active" },
    Customer = new CustomerEntity
    {
        Profile = new ProfileEntity
        {
            Name = "Customer A",
            LastName = "Test",
            ContactEmail = "customer.a@test.com",
            PhoneNumber = "0701234567"
        }
    },
    Service = new ServiceEntity
    {
        Name = "Service A",
        Description = "Service Description A",
        HourlyRate = 100m
    },
    DateRange = new DateRangeEntity
    {
        StartDate = DateTime.Now,
        EndDate = DateTime.Now.AddMonths(1)
    }
},
new()
{
    ProjectNumber = 102,
    Name = "Project B",
    TotalPrice = 20000m,
    Status = new StatusEntity { Name = "In Progress" },
    Customer = new CustomerEntity
    {
        Profile = new ProfileEntity
        {
            Name = "Customer B",
            LastName = "Test",
            ContactEmail = "customer.b@test.com",
            PhoneNumber = "0702345678"
        }
    },
    Service = new ServiceEntity
    {
        Name = "Service B",
        Description = "Service Description B",
        HourlyRate = 150m
    },
    DateRange = new DateRangeEntity
    {
        StartDate = DateTime.Now.AddMonths(-1),
        EndDate = DateTime.Now.AddMonths(2)
    }
},
new()
{
    ProjectNumber = 103,
    Name = "Project C",
    TotalPrice = 30000m,
    Status = new StatusEntity { Name = "Completed" },
    Customer = new CustomerEntity
    {
        Profile = new ProfileEntity
        {
            Name = "Customer C",
            LastName = "Test",
            ContactEmail = "customer.c@test.com",
            PhoneNumber = "0703456789"
        }
    },
    Service = new ServiceEntity
    {
        Name = "Service C",
        Description = "Service Description C",
        HourlyRate = 200m
    },
    DateRange = new DateRangeEntity
    {
        StartDate = DateTime.Now.AddMonths(-2),
        EndDate = DateTime.Now
    }
}
];



    public static readonly ProjectEmployeeEntity[] ProjectEmployeeEntityTestData =
    [
        new() {ProjectId=1, EmployeeId=1},
        new() {ProjectId=2, EmployeeId=2},
        new() {ProjectId=3, EmployeeId=3},
    ];

    public static readonly InvoiceStatusEntity[] InvoiceStatusEntityTestData =
    [
        new() {Id=1, Name="Pending"},
        new() {Id=2, Name="Paid"},
        new() {Id=3, Name="Overdue"}
    ];

    public static readonly TaskEntity[] TaskEntityTestData =
    [
        new() {Id=1, Description="Initial Setup", IsCompleted=false, ProjectId=1},
        new() {Id=2, Description="Development", IsCompleted=false, ProjectId=2},
        new() {Id=3, Description="Testing", IsCompleted=true, ProjectId=3}
    ];

    public static readonly TimesheetEntity[] TimesheetEntityTestData =
    [
        new() {Id=1, Date=new DateTime(2023, 5, 1), Hours=8, ProjectId=1, EmployeeId=1},
        new() {Id=2, Date=new DateTime(2023, 6, 1), Hours=7, ProjectId=2, EmployeeId=2},
        new() {Id=3, Date=new DateTime(2023, 7, 1), Hours=6, ProjectId=3, EmployeeId=3}
    ];


    public static readonly ExpenseEntity[] ExpenseEntityTestData =
    [
        new() {Id=1, Description="Web Development", Amount=500m, Date=new DateTime(2023, 4, 5), ProjectId=1},
        new() {Id=2, Description="Mobile", Amount=800m, Date=new DateTime(2023, 5, 10), ProjectId=2},
        new() {Id=3, Description="Consulting", Amount=1500m, Date=new DateTime(2023, 6, 15), ProjectId=3}
    ];

    public static readonly InvoiceEntity[] InvoiceEntityTestData =
    [
        new() {Id=1, Date=new DateTime(2023, 4, 1), DueDate=new DateTime(2023, 5, 1), Amount=5000m, ProjectId=1, InvoiceStatusId=1},
        new() {Id=2, Date=new DateTime(2023, 5, 5), DueDate=new DateTime(2023, 6, 5), Amount=8000m, ProjectId=2, InvoiceStatusId=2},
        new() {Id=3, Date=new DateTime(2023, 6, 10), DueDate=new DateTime(2023, 7, 10), Amount=12000m, ProjectId=3, InvoiceStatusId=3}
    ];
}
