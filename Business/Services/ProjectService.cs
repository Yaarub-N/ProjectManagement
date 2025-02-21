
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class ProjectService(IProjectRepository projectRepository)
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        // ✅ GET ALL Projects
        public async Task<ServiceResponse<IEnumerable<ProjectDTO>>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetProjectListAsync();
            if (!projects.Any())
                return new ServiceResponse<IEnumerable<ProjectDTO>>(null!, false, "No projects found.");

            return new ServiceResponse<IEnumerable<ProjectDTO>>(ProjectFactory.ToDTOList(projects), true);
        }

        // ✅ GET Project By ID
        public async Task<ServiceResponse<ProjectDTO>> GetProjectByIdAsync(int projectNumber)
        {
            if (projectNumber <= 0)
                return new ServiceResponse<ProjectDTO>(null!, false, "Invalid project number.");

            var project = await _projectRepository.GetProjectDetailsAsync(projectNumber);
            if (project == null)
                return new ServiceResponse<ProjectDTO>(null!, false, "Project not found.");

            return new ServiceResponse<ProjectDTO>(ProjectFactory.ToDTO(project), true);
        }

        // ✅ CREATE Project
        public async Task<ServiceResponse<ProjectDTO>> CreateProjectAsync(ProjectRegistrationForm projectForm)
        {
            if (projectForm == null)
                return new ServiceResponse<ProjectDTO>(null!, false, "Invalid project data.");

            var projectEntity = ProjectFactory.ToEntity(projectForm);

            var result = await _projectRepository.AddAsync(projectEntity);

            if (!result)
            {
                return new ServiceResponse<ProjectDTO>(null!, false, "Failed to create project.");
            }

            // 🟢 Logga om något är null
            Console.WriteLine($"Project Created: {projectEntity.Name}, Status: {projectEntity.Status?.Name}, Customer: {projectEntity.Customer?.Profile?.Name}, Service: {projectEntity.Service?.Name}");

            return new ServiceResponse<ProjectDTO>(ProjectFactory.ToDTO(projectEntity), true, "Project created successfully.");
        }

        // ✅ UPDATE Project
        public async Task<ServiceResponse<ProjectDTO>> UpdateProjectAsync(int projectNumber, ProjectRegistrationForm projectForm)
        {
            if (projectNumber <= 0 || projectForm == null)
                return new ServiceResponse<ProjectDTO>(null!, false, "Invalid project update request.");

            var existingProject = await _projectRepository.GetProjectDetailsAsync(projectNumber);
            if (existingProject == null)
                return new ServiceResponse<ProjectDTO>(null!, false, "Project not found.");

            existingProject.Name = projectForm.Name;
            existingProject.Description = projectForm.Description;
            existingProject.TotalPrice = projectForm.TotalPrice;
            existingProject.DateRange.StartDate = projectForm.StartDate;
            existingProject.DateRange.EndDate = projectForm.EndDate;
            existingProject.StatusId = projectForm.StatusId;
            existingProject.CustomerId = projectForm.CustomerId;
            existingProject.ServiceId = projectForm.ServiceId;

            var result = await _projectRepository.UpdateAsync(existingProject);
            return result
                ? new ServiceResponse<ProjectDTO>(ProjectFactory.ToDTO(existingProject), true, "Project updated successfully.")
                : new ServiceResponse<ProjectDTO>(null!, false, "Failed to update project.");
        }

        // ✅ DELETE Project
        public async Task<ServiceResponse<bool>> DeleteProjectAsync(int projectNumber)
        {
            if (projectNumber <= 0)
                return new ServiceResponse<bool>(false, false, "Invalid project number.");

            var existingProject = await _projectRepository.GetProjectDetailsAsync(projectNumber);
            if (existingProject == null)
                return new ServiceResponse<bool>(false, false, "Project not found.");

            var result = await _projectRepository.RemoveAsync(existingProject);
            return result
                ? new ServiceResponse<bool>(true, true, "Project deleted successfully.")
                : new ServiceResponse<bool>(false, false, "Failed to delete project.");
        }
    }
}