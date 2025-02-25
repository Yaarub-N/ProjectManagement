using Domain.DTO;
using Domain.ServiceResponses;

namespace Business.Interfaces
{
    public interface IInvoiceStatusService
    {
        Task<ServiceResponse<InvoiceStatusDTO>> CreateInvoiceStatusAsync(InvoiceStatusDTO invoiceStatusDTO);
        Task<ServiceResponse<bool>> DeleteInvoiceStatusAsync(int invoiceStatusId);
        Task<ServiceResponse<InvoiceStatusDTO>> GetInvoiceStatusByIdAsync(int invoiceStatusId);
        Task<ServiceResponse<InvoiceStatusDTO>> UpdateInvoiceStatusAsync(int invoiceStatusId, InvoiceStatusDTO invoiceStatusDTO);
    }
}