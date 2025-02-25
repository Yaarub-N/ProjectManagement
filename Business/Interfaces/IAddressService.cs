using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IAddressService
    {
        Task<ServiceResponse<AddressDTO>> CreateAddressAsync(AddressDTO addressDTO);
        Task<ServiceResponse<bool>> DeleteAddressAsync(int addressId);
        Task<ServiceResponse<AddressDTO>> GetAddressByIdAsync(int addressId);
        Task<ServiceResponse<AddressDTO>> UpdateAddressAsync(int addressId, AddressDTO addressDTO);
    }
}