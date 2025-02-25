using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceResponse<ServiceDTO>> CreateServiceAsync(ServiceDTO serviceDTO);
        Task<ServiceResponse<bool>> DeleteServiceAsync(int serviceId);
        Task<ServiceResponse<ServiceDTO>> UpdateServiceAsync(int serviceId, ServiceDTO serviceDTO);
    }
}