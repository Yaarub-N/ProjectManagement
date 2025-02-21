using Data.Entities;

namespace Data.Interfaces
{
    public interface IProjectRepository : IBaseRepository<ProjectEntity>

    {
        Task<IEnumerable<ProjectEntity>> GetProjectListAsync();
        Task<ProjectEntity?> GetProjectDetailsAsync(int projectNumber);
    }
}