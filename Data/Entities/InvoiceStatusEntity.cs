

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class InvoiceStatusEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
