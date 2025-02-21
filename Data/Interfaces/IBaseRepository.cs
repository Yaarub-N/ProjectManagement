using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> RemoveAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExsistAsync(Expression<Func<TEntity, bool>> predicate);
    }
}