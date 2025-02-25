using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class LocationFactory
    {
        public static LocationDTO ToDTO(LocationEntity location)
        {
            return new LocationDTO
            {
                Id = location.Id,
                City = location.City,
                PostalCode = location.PostalCode,
                Country = location.Country
            };
        }
        public static LocationEntity ToEntity(LocationDTO locationDTO)
        {
            return new LocationEntity
            {
                Id = locationDTO.Id,
                City = locationDTO.City,
                PostalCode = locationDTO.PostalCode,
                Country = locationDTO.Country
            };
        }
    }
}
