using Data.Context;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class DateRangeRepository(DataContext context) : BaseRepository<DateRangeEntity>(context), IDateRangeRepository
{
}
