

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;


public class InvoiceEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;
    public int InvoiceStatusId { get; set; }
    public InvoiceStatusEntity InvoiceStatus { get; set; }=null!;
    public int? CustomerId { get; set; }
    public CustomerEntity? Customer { get; set; }
}