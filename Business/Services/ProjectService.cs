using Business.Interfaces;
using Data.Context;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class ProjectService(IProjectRepository projectRepository, DataContext context) : IProjectService
    {
        private readonly DataContext _context = context;
        private readonly IProjectRepository _projectRepository = projectRepository;


        public async Task<ServiceResponse<ProjectDTO>> CreateProjectAsync(ProjectRegistrationForm form)
        {
            try
            {
                if (form == null)
                    return new ServiceResponse<ProjectDTO>(null!, false, "Invalid project data.");

                var projectEntity = ProjectFactory.ToEntity(form);
                var result = await _projectRepository.AddAsync(projectEntity);

                if (!result)
                    return new ServiceResponse<ProjectDTO>(null!, false, "Failed to create project.");

                return new ServiceResponse<ProjectDTO>(ProjectFactory.ToDTO(projectEntity), true, "Project created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProjectDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }


        public async Task<ServiceResponse<IEnumerable<ProjectDTO>>> GetAllProjectsAsync()
        {
            try
            {
                var projects = await _projectRepository.GetProjectListAsync();
                if (!projects.Any())
                    return new ServiceResponse<IEnumerable<ProjectDTO>>(null!, false, "No projects found.");

                return new ServiceResponse<IEnumerable<ProjectDTO>>(ProjectFactory.ToDTOList(projects), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<IEnumerable<ProjectDTO>>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<ProjectDTO>> GetProjectByIdAsync(int projectNumber)
        {
            try
            {
                if (projectNumber <= 0)
                    return new ServiceResponse<ProjectDTO>(null!, false, "Invalid project number.");

                var project = await _context.Projects
                    .Include(p => p.Status)
                    .Include(p => p.Customer).ThenInclude(c => c!.Profile)
                    .Include(p => p.Service)
                    .Include(p => p.DateRange)
                    .Include(p => p.ProjectManager)
                    .FirstOrDefaultAsync(p => p.ProjectNumber == projectNumber);

                if (project == null)
                    return new ServiceResponse<ProjectDTO>(null!, false, "Project not found.");

                return new ServiceResponse<ProjectDTO>(ProjectFactory.ToDTO(project), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProjectDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }
  

        public async Task<ServiceResponse<ProjectDTO>> UpdateProjectAsync(int projectNumber, ProjectRegistrationForm form)
        {
            try
            {
                if (projectNumber <= 0 || form == null)
                    return new ServiceResponse<ProjectDTO>(null!, false, "Invalid project update request.");

                var existingProject = await _projectRepository.GetProjectDetailsAsync(projectNumber);
                if (existingProject == null)
                    return new ServiceResponse<ProjectDTO>(null!, false, "Project not found.");

                existingProject.Name = form.Name;
                existingProject.Description = form.Description;
                existingProject.TotalPrice = form.TotalPrice;
                existingProject.DateRange.StartDate = form.StartDate;
                existingProject.DateRange.EndDate = form.EndDate;
                existingProject.StatusId = form.StatusId;
                existingProject.CustomerId = form.CustomerId;
                existingProject.ServiceId = form.ServiceId;
                existingProject.ProjectManagerId = form.ProjectManagerId;

                var result = await _projectRepository.UpdateAsync(existingProject);
                if (!result)
                    return new ServiceResponse<ProjectDTO>(null!, false, "Failed to update project.");

                return new ServiceResponse<ProjectDTO>(ProjectFactory.ToDTO(existingProject), true, "Project updated successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProjectDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteProjectAsync(int projectNumber)
        {
            try
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
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
