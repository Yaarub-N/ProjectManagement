

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public ProfileEntity? Profile { get; set; }
    public int AddressId { get; set; }
    public AddressEntity? Address { get; set; }

    public ICollection<ProjectEntity> Projects { get; set; } = [];
}

