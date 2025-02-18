

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class DateRangeEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public ICollection<ProjectEntity> Projects { get; set; } = [];
}
