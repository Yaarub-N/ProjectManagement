
using System.ComponentModel.DataAnnotations;


namespace Data.Entities;

public class ExpenseEntity
{
    [Key]
    public int Id { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int ProjectId { get; set; }
    public ProjectEntity? Project { get; set; }
}
