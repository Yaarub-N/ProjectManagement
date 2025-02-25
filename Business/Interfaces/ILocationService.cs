using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface ILocationService
    {
        Task<ServiceResponse<LocationDTO>> CreateLocationAsync(LocationDTO locationDTO);
        Task<ServiceResponse<bool>> DeleteLocationAsync(int locationId);
        Task<ServiceResponse<LocationDTO>> GetLocationByIdAsync(int locationId);
        Task<ServiceResponse<LocationDTO>> UpdateLocationAsync(int locationId, LocationDTO locationDTO);
    }
}