using Data.Context;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class TimesheetRepository(DataContext context) : BaseRepository<TimesheetEntity>(context), ITimesheetRepository
{
}
