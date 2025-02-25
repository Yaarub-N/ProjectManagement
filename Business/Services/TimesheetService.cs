using Business.Interfaces;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class TimesheetService(ITimesheetRepository timesheetRepository) : ITimesheetService
    {
        public async Task<ServiceResponse<TimesheetDTO>> CreateTimesheetAsync(TimesheetDTO timesheetDTO)
        {
            try
            {
                if (timesheetDTO == null)
                    return new ServiceResponse<TimesheetDTO>(null!, false, "Invalid timesheet data.");

                var timesheetEntity = TimesheetFactory.ToEntity(timesheetDTO);
                var result = await timesheetRepository.AddAsync(timesheetEntity);

                if (!result)
                    return new ServiceResponse<TimesheetDTO>(null!, false, "Failed to create timesheet.");

                return new ServiceResponse<TimesheetDTO>(TimesheetFactory.ToDTO(timesheetEntity), true, "Timesheet created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<TimesheetDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<TimesheetDTO>> GetTimesheetByIdAsync(int timesheetId)
        {
            try
            {
                var timesheet = await timesheetRepository.GetAsync(t => t.Id == timesheetId);
                if (timesheet == null)
                    return new ServiceResponse<TimesheetDTO>(null!, false, "Timesheet not found.");

                return new ServiceResponse<TimesheetDTO>(TimesheetFactory.ToDTO(timesheet), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<TimesheetDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<TimesheetDTO>> UpdateTimesheetAsync(int timesheetId, TimesheetDTO timesheetDTO)
        {
            try
            {
                if (timesheetId <= 0 || timesheetDTO == null)
                    return new ServiceResponse<TimesheetDTO>(null!, false, "Invalid timesheet update request.");

                var existingTimesheet = await timesheetRepository.GetAsync(t => t.Id == timesheetId);
                if (existingTimesheet == null)
                    return new ServiceResponse<TimesheetDTO>(null!, false, "Timesheet not found.");

                existingTimesheet.Date = timesheetDTO.Date;
                existingTimesheet.Hours = timesheetDTO.Hours;
                existingTimesheet.ProjectId = timesheetDTO.ProjectId;
                existingTimesheet.EmployeeId = timesheetDTO.EmployeeId;

                var result = await timesheetRepository.UpdateAsync(existingTimesheet);
                return result
                    ? new ServiceResponse<TimesheetDTO>(TimesheetFactory.ToDTO(existingTimesheet), true, "Timesheet updated successfully.")
                    : new ServiceResponse<TimesheetDTO>(null!, false, "Failed to update timesheet.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<TimesheetDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteTimesheetAsync(int timesheetId)
        {
            try
            {
                var existingTimesheet = await timesheetRepository.GetAsync(t => t.Id == timesheetId);
                if (existingTimesheet == null)
                    return new ServiceResponse<bool>(false, false, "Timesheet not found.");

                var result = await timesheetRepository.RemoveAsync(existingTimesheet);
                return result
                    ? new ServiceResponse<bool>(true, true, "Timesheet deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete timesheet.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
