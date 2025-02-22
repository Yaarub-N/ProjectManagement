﻿
using Data.Context;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class ProjectService(IProjectRepository projectRepository, DataContext context)
    {
        private readonly DataContext _context = context;
        private readonly IProjectRepository _projectRepository = projectRepository;


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
                return new ServiceResponse<IEnumerable<ProjectDTO>>(null!, false, $"Somthing went wrong: {e.Message}");
            }

        }


        public async Task<ServiceResponse<ProjectDTO>> GetProjectByIdAsync(int projectNumber)
        {
            try
            {
                // Validera projectNumber
                if (projectNumber <= 0)
                {
                    return new ServiceResponse<ProjectDTO>(null!, false, "Invalid project number.");
                }

                // Hämta projektet från databasen
                var project = await _context.Projects
                    .Include(p => p.Status)
                    .Include(p => p.Customer)
                    .Include(p => p.Service)
                    .Include(p => p.DateRange) // Se till att DateRange ingår
                    .Include(p => p.ProjectManager) // Se till att ProjectManager ingår
                    .FirstOrDefaultAsync(p => p.ProjectNumber == projectNumber);

                // Kontrollera om projektet finns
                if (project == null)
                {
                    return new ServiceResponse<ProjectDTO>(null!, false, "Project not found.");
                }

                // Använd ProjectFactory för att mappa ProjectEntity till ProjectDTO
                var projectDTO = ProjectFactory.ToDTO(project);

                // Returnera ett lyckat svar med projektet
                return new ServiceResponse<ProjectDTO>(projectDTO, true);
            }
            catch (Exception e)
            {
                // Hantera eventuella undantag och returnera ett felmeddelande
                return new ServiceResponse<ProjectDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }


        public async Task<ServiceResponse<ProjectDTO>> CreateProjectAsync(ProjectRegistrationForm projectForm)
        {
            try
            {


                if (projectForm == null)
                    return new ServiceResponse<ProjectDTO>(null!, false, "Invalid project data.");

                var projectEntity = ProjectFactory.ToEntity(projectForm);

                var result = await _projectRepository.AddAsync(projectEntity);

                if (!result)
                {
                    return new ServiceResponse<ProjectDTO>(null!, false, "Failed to create project.");
                }

                Console.WriteLine($"Project Created: {projectEntity.Name}, Status: {projectEntity.Status?.Name}, Customer: {projectEntity.Customer?.Profile?.Name}, Service: {projectEntity.Service?.Name}");

                return new ServiceResponse<ProjectDTO>(ProjectFactory.ToDTO(projectEntity), true, "Project created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProjectDTO>(null!, false, $"Somthing went wrong: {e.Message}");
            }

        }
        public async Task<ServiceResponse<ProjectDTO>> UpdateProjectAsync(int projectNumber, ProjectRegistrationForm projectForm)
        {
            try
            {
                if (projectNumber <= 0 || projectForm == null)
                    return new ServiceResponse<ProjectDTO>(null!, false, "Invalid project update request.");

                var existingProject = await _projectRepository.GetProjectDetailsAsync(projectNumber);
                if (existingProject == null)
                    return new ServiceResponse<ProjectDTO>(null!, false, "Project not found.");

                // Validering: Se till att StatusId, CustomerId och ServiceId är giltiga.
                if (projectForm.StatusId <= 0 || projectForm.CustomerId <= 0 || projectForm.ServiceId <= 0)
                {
                    return new ServiceResponse<ProjectDTO>(null!, false, "Invalid status, customer, or service ID.");
                }

                existingProject.Name = projectForm.Name;
                existingProject.Description = projectForm.Description;
                existingProject.TotalPrice = projectForm.TotalPrice;
                existingProject.DateRange.StartDate = projectForm.StartDate;
                existingProject.DateRange.EndDate = projectForm.EndDate;
                existingProject.StatusId = projectForm.StatusId;
                existingProject.CustomerId = projectForm.CustomerId;
                existingProject.ServiceId = projectForm.ServiceId;

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
                return new ServiceResponse<bool>(false, $"Somthing went wrong: {e.Message}");
            }

        }
    }
}