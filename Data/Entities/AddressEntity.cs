

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class AddressEntity
{
    [Key]
    public int Id { get; set; }
    public string Street { get; set; } = null!;
    public int LocationId { get; set; }
    public LocationEntity? Location { get; set; }
}
