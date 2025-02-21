using Data.Context;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _db = context.Set<TEntity>();

    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        try
        {
            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        try
        {
            _db.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public virtual async Task<bool> RemoveAsync(TEntity entity)
    {
        try
        {
            _db.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _db.ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _db.FirstOrDefaultAsync(expression);
    }

    public virtual async Task<bool> ExsistAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _db.AnyAsync(predicate);
    }
}
