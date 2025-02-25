using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IDateRangeService
    {
        Task<ServiceResponse<DateRangeDTO>> CreateDateRangeAsync(DateRangeDTO dateRangeDTO);
        Task<ServiceResponse<bool>> DeleteDateRangeAsync(int dateRangeId);
        Task<ServiceResponse<DateRangeDTO>> GetDateRangeByIdAsync(int dateRangeId);
        Task<ServiceResponse<DateRangeDTO>> UpdateDateRangeAsync(int dateRangeId, DateRangeDTO dateRangeDTO);
    }
}