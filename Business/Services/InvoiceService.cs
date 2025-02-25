using Business.Interfaces;
using Data.Interfaces;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class InvoiceService(IInvoiceRepository invoiceRepository) : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

        public async Task<ServiceResponse<InvoiceDTO>> CreateInvoiceAsync(InvoiceDTO invoiceDTO)
        {
            try
            {
                if (invoiceDTO == null)
                    return new ServiceResponse<InvoiceDTO>(null!, false, "Invalid invoice data.");

                var invoiceEntity = InvoiceFactory.ToEntity(invoiceDTO);
                var result = await _invoiceRepository.AddAsync(invoiceEntity);

                if (!result)
                    return new ServiceResponse<InvoiceDTO>(null!, false, "Failed to create invoice.");

                return new ServiceResponse<InvoiceDTO>(InvoiceFactory.ToDTO(invoiceEntity), true, "Invoice created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<InvoiceDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<InvoiceDTO>> GetInvoiceByIdAsync(int invoiceId)
        {
            try
            {
                var invoice = await _invoiceRepository.GetAsync(i => i.Id == invoiceId);
                if (invoice == null)
                    return new ServiceResponse<InvoiceDTO>(null!, false, "Invoice not found.");

                return new ServiceResponse<InvoiceDTO>(InvoiceFactory.ToDTO(invoice), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<InvoiceDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<InvoiceDTO>> UpdateInvoiceAsync(int invoiceId, InvoiceDTO invoiceDTO)
        {
            try
            {
                if (invoiceId <= 0 || invoiceDTO == null)
                    return new ServiceResponse<InvoiceDTO>(null!, false, "Invalid invoice update request.");

                var existingInvoice = await _invoiceRepository.GetAsync(i => i.Id == invoiceId);
                if (existingInvoice == null)
                    return new ServiceResponse<InvoiceDTO>(null!, false, "Invoice not found.");

                existingInvoice.Date = invoiceDTO.Date;
                existingInvoice.DueDate = invoiceDTO.DueDate;
                existingInvoice.Amount = invoiceDTO.Amount;
                existingInvoice.ProjectId = invoiceDTO.ProjectId;
                existingInvoice.InvoiceStatusId = invoiceDTO.InvoiceStatusId;
                existingInvoice.CustomerId = invoiceDTO.CustomerId;

                var result = await _invoiceRepository.UpdateAsync(existingInvoice);
                return result
                    ? new ServiceResponse<InvoiceDTO>(InvoiceFactory.ToDTO(existingInvoice), true, "Invoice updated successfully.")
                    : new ServiceResponse<InvoiceDTO>(null!, false, "Failed to update invoice.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<InvoiceDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteInvoiceAsync(int invoiceId)
        {
            try
            {
                var existingInvoice = await _invoiceRepository.GetAsync(i => i.Id == invoiceId);
                if (existingInvoice == null)
                    return new ServiceResponse<bool>(false, false, "Invoice not found.");

                var result = await _invoiceRepository.RemoveAsync(existingInvoice);
                return result
                    ? new ServiceResponse<bool>(true, true, "Invoice deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete invoice.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
