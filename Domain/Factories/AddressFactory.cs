using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class AddressFactory
    {
        public static AddressDTO ToDTO(AddressEntity address)
        {
            return new AddressDTO
            {
                Id = address.Id,
                Street = address.Street,
                Location = LocationFactory.ToDTO(address.Location!)
            };
        }

        public static AddressEntity ToEntity(AddressDTO addressDTO)
        {
            return new AddressEntity
            {
                Id = addressDTO.Id,
                Street = addressDTO.Street,
                LocationId = addressDTO.Location.Id 
            };
        }
    }
}
