

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class LocationEntity
{
    [Key]
    public int Id { get; set; }
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Country { get; set; } = null!;
}
