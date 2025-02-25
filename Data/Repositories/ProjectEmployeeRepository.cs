using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;  
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class ProjectEmployeeRepository(DataContext context) : BaseRepository<ProjectEmployeeEntity>(context), IProjectEmployeeRepository
    {
        
        public virtual async Task<IEnumerable<ProjectEmployeeEntity>> GetAllAsyncWithPredicate(Expression<Func<ProjectEmployeeEntity, bool>> predicate)
        {
            
            return await _db.Where(predicate).ToListAsync();
        }
    }
}
