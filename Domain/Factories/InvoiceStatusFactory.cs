using Data.Entities;
using Domain.DTO;

namespace Domain.Factories
{
    public static class InvoiceStatusFactory
    {
        public static InvoiceStatusDTO ToDTO(InvoiceStatusEntity invoiceStatus)
        {
            return new InvoiceStatusDTO
            {
                Id = invoiceStatus.Id,
                Name = invoiceStatus.Name
            };
        }

        public static InvoiceStatusEntity ToEntity(InvoiceStatusDTO invoiceStatusDTO)
        {
            return new InvoiceStatusEntity
            {
                Id = invoiceStatusDTO.Id,
                Name = invoiceStatusDTO.Name
            };
        }
    }
}
