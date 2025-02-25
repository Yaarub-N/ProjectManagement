namespace Domain.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public LocationDTO Location { get; set; } = new LocationDTO();
        public int LocationId { get; set; }
    }
}
