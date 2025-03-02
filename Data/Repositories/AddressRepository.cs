using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class AddressRepository(DataContext context) : BaseRepository<AddressEntity>(context), IAddressRepository
{
    public override async Task<IEnumerable<AddressEntity>> GetAllAsync()
    {
        return await _db
                        .Include(a => a.Location) 
                       .ToListAsync();
                     
    }
 
}
