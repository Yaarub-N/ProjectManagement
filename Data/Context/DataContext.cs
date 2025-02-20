using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<DateRangeEntity> DateRanges { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<LocationEntity> Locations { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<ExpenseEntity> Expenses { get; set; }
    public DbSet<InvoiceEntity> Invoices { get; set; }
    public DbSet<InvoiceStatusEntity> InvoiceStatuses { get; set; }
    public DbSet<ProjectEmployeeEntity> ProjectEmployees { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<TimesheetEntity> Timesheets { get; set; }
    public DbSet<StatusEntity> Statuses { get; set; }
    public DbSet<ProfileEntity> Profiles { get; set; }


    //Av Chat Gpt-4o
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfigurera många-till-många-relationen mellan Project och Employee
        modelBuilder.Entity<ProjectEmployeeEntity>()
            .HasKey(pe => new { pe.ProjectId, pe.EmployeeId }); // Sammansatt primärnyckel

        modelBuilder.Entity<ProjectEmployeeEntity>()
            .HasOne(pe => pe.Project)
            .WithMany(p => p.ProjectEmployees)
            .HasForeignKey(pe => pe.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProjectEmployeeEntity>()
            .HasOne(pe => pe.Employee)
            .WithMany(e => e.ProjectEmployees)
            .HasForeignKey(pe => pe.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Konfigurera ProjectNumber som unik men inte primärnyckel
        modelBuilder.Entity<ProjectEntity>()
            .HasIndex(p => p.ProjectNumber)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
