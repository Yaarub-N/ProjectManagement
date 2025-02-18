

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;



    public class ProjectEntity
    {
        [Key]
        public int ProjectNumber { get; set; }

    public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal TotalPrice { get; set; }

        public int DateRangeId { get; set; }
    public DateRangeEntity DateRange { get; set; } = null!;

        public int StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;

        public int? CustomerId { get; set; }
        public CustomerEntity? Customer { get; set; }

        public int ServiceId { get; set; }
        public ServiceEntity? Service { get; set; }

        public int? ProjectManagerId { get; set; }
        public EmployeeEntity? ProjectManager { get; set; }

        public ICollection<ProjectEmployeeEntity> ProjectEmployees { get; set; } = [];
        public ICollection<ExpenseEntity> Expenses { get; set; } = [];
        public ICollection<InvoiceEntity> Invoices { get; set; } = [];
        public ICollection<TaskEntity> Tasks { get; set; } = [];
        public ICollection<TimesheetEntity> Timesheets { get; set; } = [];
    }
