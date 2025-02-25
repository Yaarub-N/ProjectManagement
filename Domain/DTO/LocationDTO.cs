namespace Domain.DTO
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
