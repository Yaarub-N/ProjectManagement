namespace Domain.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string ProfileName { get; set; } = string.Empty;
        public string ProfileEmail { get; set; } = string.Empty;
        public string AddressStreet { get; set; } = string.Empty;
        public string AddressCity { get; set; } = string.Empty;
        public string AddressPostalCode { get; set; } = string.Empty;
        public string AddressCountry { get; set; } = string.Empty;
    }
}
