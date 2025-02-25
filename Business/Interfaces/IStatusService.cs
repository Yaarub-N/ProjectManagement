using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IStatusService
    {
        Task<ServiceResponse<StatusDTO>> CreateStatusAsync(StatusDTO statusDTO);
        Task<ServiceResponse<bool>> DeleteStatusAsync(int statusId);
        Task<ServiceResponse<StatusDTO>> GetStatusByIdAsync(int statusId);
        Task<ServiceResponse<StatusDTO>> UpdateStatusAsync(int statusId, StatusDTO statusDTO);
    }
}