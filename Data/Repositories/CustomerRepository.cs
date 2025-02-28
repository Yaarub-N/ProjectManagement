using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{
    public  override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        return await _context.Customers
            .Include(c => c.Profile) 
            .Include(c => c.Address) 
            .ToListAsync();
    }
}

