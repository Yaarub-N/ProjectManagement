using Data.Context;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class InvoiceRepository(DataContext context) : BaseRepository<InvoiceEntity>(context), IInvoiceRepository
{
}
