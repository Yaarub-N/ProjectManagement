﻿using Data.Context;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class EmployeeRepository(DataContext context) : BaseRepository<EmployeeEntity>(context), IEmployeeRepository
{
}
