using Data.Context;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ProfileRepository(DataContext context) : BaseRepository<ProfileEntity>(context), IProfileRepository
{
}
