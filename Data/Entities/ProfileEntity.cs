
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ProfileEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? LastName { get; set; }
    public string ContactEmail { get; set; } = null!;
    public string? PhoneNumber { get; set; }
}
