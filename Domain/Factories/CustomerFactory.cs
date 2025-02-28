using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class CustomerFactory
    {
        public static CustomerDTO ToDTO(CustomerEntity customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                ProfileName = customer.Profile?.Name ?? "Unknown",
                ProfileEmail = customer.Profile?.ContactEmail ?? "No email",
                AddressStreet = customer.Address?.Street ?? "Unknown Street",
                AddressCity = customer.Address?.Location?.City ?? "Unknown City",
                AddressPostalCode = customer.Address?.Location?.PostalCode ?? "Unknown PostalCode",
                AddressCountry = customer.Address?.Location?.Country ?? "Unknown Country"
            };
        }

        public static CustomerEntity ToEntity(CustomerDTO customerDTO)
        {
            return new CustomerEntity
            {
                Id = customerDTO.Id,
                Profile = new ProfileEntity
                {
                    Name = customerDTO.ProfileName,
                    ContactEmail = customerDTO.ProfileEmail
                },
                Address = new AddressEntity
                {
                    Street = customerDTO.AddressStreet,
                    Location = new LocationEntity
                    {
                        City = customerDTO.AddressCity,
                        PostalCode = customerDTO.AddressPostalCode,
                        Country = customerDTO.AddressCountry
                    }
                }
            };
        }

        public static IEnumerable<CustomerDTO> ToDTOList(IEnumerable<CustomerEntity> customers)
        {
            return customers.Select(ToDTO);
        }
    }
}