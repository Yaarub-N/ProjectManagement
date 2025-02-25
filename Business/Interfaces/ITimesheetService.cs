using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface ITimesheetService
    {
        Task<ServiceResponse<TimesheetDTO>> CreateTimesheetAsync(TimesheetDTO timesheetDTO);
        Task<ServiceResponse<bool>> DeleteTimesheetAsync(int timesheetId);
        Task<ServiceResponse<TimesheetDTO>> GetTimesheetByIdAsync(int timesheetId);
        Task<ServiceResponse<TimesheetDTO>> UpdateTimesheetAsync(int timesheetId, TimesheetDTO timesheetDTO);
    }
}