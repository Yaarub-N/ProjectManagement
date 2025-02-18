

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class EmployeeEntity
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int RoleId { get; set; }
    public RoleEntity? Role { get; set; }

    public ICollection<ProjectEmployeeEntity> ProjectEmployees { get; set; } = [];
}
