using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IProjectEmployeeRepository : IBaseRepository<ProjectEmployeeEntity>
    {
        Task<IEnumerable<ProjectEmployeeEntity>> GetAllAsyncWithPredicate(Expression<Func<ProjectEmployeeEntity, bool>> predicate);
    }
}
