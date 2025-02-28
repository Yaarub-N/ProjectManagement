
using Data.Entities;
using Domain.DTO;
//chat Gpt4o
namespace Domain.Factories
{
    public static class ProjectFactory
    {
        public static ProjectDTO ToDTO(ProjectEntity project)
        {
            var statusName = project.Status?.Name ?? "Unknown Status";
            var customerName = project.Customer?.Profile?.Name ?? "Unknown Customer";
            var serviceName = project.Service?.Name ?? "Unknown Service";
            var serviceHourlyRate = project.Service?.HourlyRate ?? 0;
            var startDate = project.DateRange?.StartDate ?? DateTime.MinValue;
            var endDate = project.DateRange?.EndDate ?? DateTime.MinValue;
            var projectManagerName = (project.ProjectManager != null)
                ? $"{project.ProjectManager.FirstName} {project.ProjectManager.LastName}"
                : "No Manager";

            return new ProjectDTO
            {
                ProjectNumber = project.ProjectNumber,
                Name = project.Name ?? "N/A",
                Description = project.Description ?? "No Description",
                TotalPrice = project.TotalPrice,
                Status = statusName,
                CustomerName = customerName,
                ServiceName = serviceName,
                ServiceHourlyRate = serviceHourlyRate,
                StartDate = startDate,
                EndDate = endDate,
                ProjectManagerName = projectManagerName
            };
        }


        public static IEnumerable<ProjectDTO> ToDTOList(IEnumerable<ProjectEntity> projects)
        {
            return projects.Select(ToDTO);
        }


        public static ProjectEntity ToEntity(ProjectRegistrationForm projectForm)
        {
            return new ProjectEntity
            {
                Name = projectForm.Name,
                Description = projectForm.Description,
                TotalPrice = projectForm.TotalPrice,
                StatusId = projectForm.StatusId,
                CustomerId = projectForm.CustomerId,
                ServiceId = projectForm.ServiceId,
                DateRange = new DateRangeEntity
                {
                    StartDate = projectForm.StartDate,
                    EndDate = projectForm.EndDate
                },
                ProjectManagerId = projectForm.ProjectManagerId // Kontrollera att detta sätts korrekt
            };
        }

    }
}
