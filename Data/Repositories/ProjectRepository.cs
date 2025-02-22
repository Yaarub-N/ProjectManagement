using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
    {
        public async Task<IEnumerable<ProjectEntity>> GetProjectListAsync()
        {
            return await _db
                .Include(p => p.Status)
                .Include(p => p.DateRange)
                .Include(p => p.Customer)
                    .ThenInclude(c => c!.Profile)
                .Include(p => p.Service)
                .ToListAsync();
        }

        public async Task<ProjectEntity?> GetProjectDetailsAsync(int projectNumber)
        {
            return await _db
     .Where(p => p.ProjectNumber == projectNumber)
        .Include(p => p.Status)
        .Include(p => p.Customer)
            .ThenInclude(c => c!.Profile)
        .Include(p => p.Service)
        .Include(p => p.DateRange)
        .Include(p => p.ProjectManager)  
        .FirstOrDefaultAsync();
        }
    }
}