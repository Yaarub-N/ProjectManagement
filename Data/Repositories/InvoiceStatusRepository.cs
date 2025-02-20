using Data.Context;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class InvoiceStatusRepository(DataContext context) : BaseRepository<InvoiceStatusEntity>(context), IInvoiceStatusRepository
{
}
