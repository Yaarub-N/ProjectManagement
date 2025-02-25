        namespace Domain.DTO;


public class ProjectRegistrationForm
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal TotalPrice { get; set; }
    public int StatusId { get; set; }
    public int CustomerId { get; set; }
    public int ServiceId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

