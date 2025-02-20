using Data.Context;
using Data.Repositories;

namespace Data.Entities;

public class ProjectEmployeeRepository(DataContext context) : BaseRepository<ProjectEmployeeEntity>(context), IProjectEmployeeRepository
{
}
