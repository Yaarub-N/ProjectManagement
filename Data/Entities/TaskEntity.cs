

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class TaskEntity
{
    [Key]
    public int Id { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;
}

