using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/addresses")]
    public class AddressController(AddressService addressService) : ControllerBase
    {
        private readonly AddressService _addressService = addressService;

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressDTO addressDTO)
        {
            var response = await _addressService.CreateAddressAsync(addressDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            var response = await _addressService.GetAllAddressesAsync();
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetAddress(int addressId)
        {
            var response = await _addressService.GetAddressByIdAsync(addressId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, [FromBody] AddressDTO addressDTO)
        {
            var response = await _addressService.UpdateAddressAsync(addressId, addressDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            var response = await _addressService.DeleteAddressAsync(addressId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}
