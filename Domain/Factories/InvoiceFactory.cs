using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class InvoiceFactory
    {
        public static InvoiceDTO ToDTO(InvoiceEntity invoice)
        {
            return new InvoiceDTO
            {
                Id = invoice.Id,
                Date = invoice.Date,
                DueDate = invoice.DueDate,
                Amount = invoice.Amount,
                ProjectId = invoice.ProjectId,
                InvoiceStatusId = invoice.InvoiceStatusId,
                CustomerId = invoice.CustomerId
            };
        }

        public static InvoiceEntity ToEntity(InvoiceDTO invoiceDTO)
        {
            return new InvoiceEntity
            {
                Id = invoiceDTO.Id,
                Date = invoiceDTO.Date,
                DueDate = invoiceDTO.DueDate,
                Amount = invoiceDTO.Amount,
                ProjectId = invoiceDTO.ProjectId,
                InvoiceStatusId = invoiceDTO.InvoiceStatusId,
                CustomerId = invoiceDTO.CustomerId
            };
        }
    }
}
