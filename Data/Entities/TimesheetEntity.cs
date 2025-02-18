using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities;

public class TimesheetEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Hours { get; set; }
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;
    public int EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; } = null!;
}
