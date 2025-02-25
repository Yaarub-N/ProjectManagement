using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class TimesheetFactory
    {
        public static TimesheetDTO ToDTO(TimesheetEntity timesheet)
        {
            return new TimesheetDTO
            {
                Id = timesheet.Id,
                Date = timesheet.Date,
                Hours = timesheet.Hours,
                ProjectId = timesheet.ProjectId,
                EmployeeId = timesheet.EmployeeId
            };
        }

        public static TimesheetEntity ToEntity(TimesheetDTO timesheetDTO)
        {
            return new TimesheetEntity
            {
                Id = timesheetDTO.Id,
                Date = timesheetDTO.Date,
                Hours = timesheetDTO.Hours,
                ProjectId = timesheetDTO.ProjectId,
                EmployeeId = timesheetDTO.EmployeeId
            };
        }
    }
}
