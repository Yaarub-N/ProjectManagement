namespace Domain.DTO
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  
        public string? LastName { get; set; }  
        public string ContactEmail { get; set; } = string.Empty; 
        public string? PhoneNumber { get; set; } 
    }
}
