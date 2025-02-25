using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IInvoiceService
    {
        Task<ServiceResponse<InvoiceDTO>> CreateInvoiceAsync(InvoiceDTO invoiceDTO);
        Task<ServiceResponse<bool>> DeleteInvoiceAsync(int invoiceId);
        Task<ServiceResponse<InvoiceDTO>> GetInvoiceByIdAsync(int invoiceId);
        Task<ServiceResponse<InvoiceDTO>> UpdateInvoiceAsync(int invoiceId, InvoiceDTO invoiceDTO);
    }
}