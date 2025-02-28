using Business.Interfaces;
using Business.Services;
using Data.Context;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
}

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceStatusRepository, InvoiceStatusRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IProjectEmployeeRepository, ProjectEmployeeRepository>();
builder.Services.AddScoped<IDateRangeRepository, DateRangeRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>();


//chat gpt4o
builder.Services.AddScoped<IProjectService, ProjectService>();


builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<InvoiceStatusService>();
builder.Services.AddScoped<ExpenseService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<StatusService>();
builder.Services.AddScoped<ProjectEmployeeService>();
builder.Services.AddScoped<DateRangeService>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<TimesheetService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()  
                  .AllowAnyMethod()  
                  .AllowAnyHeader(); 
        });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
