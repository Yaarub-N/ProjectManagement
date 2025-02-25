using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class AddressService(IAddressRepository addressRepository)
    {
        private readonly IAddressRepository _addressRepository = addressRepository;

        public async Task<ServiceResponse<AddressDTO>> CreateAddressAsync(AddressDTO addressDTO)
        {
            try
            {
                if (addressDTO == null)
                    return new ServiceResponse<AddressDTO>(null!, false, "Invalid address data.");
                var addressEntity = AddressFactory.ToEntity(addressDTO);
                var result = await _addressRepository.AddAsync(addressEntity);

                if (!result)
                    return new ServiceResponse<AddressDTO>(null!, false, "Failed to create address.");

                return new ServiceResponse<AddressDTO>(AddressFactory.ToDTO(addressEntity), true, "Address created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<AddressDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<AddressDTO>> GetAddressByIdAsync(int addressId)
        {
            try
            {
                if (addressId <= 0)
                    return new ServiceResponse<AddressDTO>(null!, false, "Invalid address ID.");

                var address = await _addressRepository.GetAsync(a => a.Id == addressId);
                if (address == null)
                    return new ServiceResponse<AddressDTO>(null!, false, "Address not found.");

                return new ServiceResponse<AddressDTO>(AddressFactory.ToDTO(address), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<AddressDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<AddressDTO>> UpdateAddressAsync(int addressId, AddressDTO addressDTO)
        {
            try
            {
                if (addressId <= 0 || addressDTO == null)
                    return new ServiceResponse<AddressDTO>(null!, false, "Invalid address update request.");

                var existingAddress = await _addressRepository.GetAsync(a => a.Id == addressId);
                if (existingAddress == null)
                    return new ServiceResponse<AddressDTO>(null!, false, "Address not found.");

                existingAddress.Street = addressDTO.Street;
                existingAddress.LocationId = addressDTO.LocationId;

                var result = await _addressRepository.UpdateAsync(existingAddress);
                return result
                    ? new ServiceResponse<AddressDTO>(AddressFactory.ToDTO(existingAddress), true, "Address updated successfully.")
                    : new ServiceResponse<AddressDTO>(null!, false, "Failed to update address.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<AddressDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }


        public async Task<ServiceResponse<bool>> DeleteAddressAsync(int addressId)
        {
            try
            {
                if (addressId <= 0)
                    return new ServiceResponse<bool>(false, false, "Invalid address ID.");

                var existingAddress = await _addressRepository.GetAsync(a => a.Id == addressId);
                if (existingAddress == null)
                    return new ServiceResponse<bool>(false, false, "Address not found.");

                var result = await _addressRepository.RemoveAsync(existingAddress);
                return result
                    ? new ServiceResponse<bool>(true, true, "Address deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete address.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
