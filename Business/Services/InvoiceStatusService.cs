using Data.Interfaces;
using Data.Repositories;
using Domain.DTO;
using Domain.Factories;
using Domain.ServiceResponses;

namespace Business.Services
{
    public class InvoiceStatusService
    {
        private readonly IInvoiceStatusRepository _invoiceStatusRepository;

        public InvoiceStatusService(IInvoiceStatusRepository invoiceStatusRepository)
        {
            _invoiceStatusRepository = invoiceStatusRepository;
        }

        public async Task<ServiceResponse<InvoiceStatusDTO>> CreateInvoiceStatusAsync(InvoiceStatusDTO invoiceStatusDTO)
        {
            try
            {
                if (invoiceStatusDTO == null)
                    return new ServiceResponse<InvoiceStatusDTO>(null!, false, "Invalid invoice status data.");

                var invoiceStatusEntity = InvoiceStatusFactory.ToEntity(invoiceStatusDTO);
                var result = await _invoiceStatusRepository.AddAsync(invoiceStatusEntity);

                if (!result)
                    return new ServiceResponse<InvoiceStatusDTO>(null!, false, "Failed to create invoice status.");

                return new ServiceResponse<InvoiceStatusDTO>(InvoiceStatusFactory.ToDTO(invoiceStatusEntity), true, "Invoice status created successfully.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<InvoiceStatusDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<InvoiceStatusDTO>> GetInvoiceStatusByIdAsync(int invoiceStatusId)
        {
            try
            {
                var invoiceStatus = await _invoiceStatusRepository.GetAsync(i => i.Id == invoiceStatusId);
                if (invoiceStatus == null)
                    return new ServiceResponse<InvoiceStatusDTO>(null!, false, "Invoice status not found.");

                return new ServiceResponse<InvoiceStatusDTO>(InvoiceStatusFactory.ToDTO(invoiceStatus), true);
            }
            catch (Exception e)
            {
                return new ServiceResponse<InvoiceStatusDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<InvoiceStatusDTO>> UpdateInvoiceStatusAsync(int invoiceStatusId, InvoiceStatusDTO invoiceStatusDTO)
        {
            try
            {
                if (invoiceStatusId <= 0 || invoiceStatusDTO == null)
                    return new ServiceResponse<InvoiceStatusDTO>(null!, false, "Invalid invoice status update request.");

                var existingInvoiceStatus = await _invoiceStatusRepository.GetAsync(i => i.Id == invoiceStatusId);
                if (existingInvoiceStatus == null)
                    return new ServiceResponse<InvoiceStatusDTO>(null!, false, "Invoice status not found.");

                existingInvoiceStatus.Name = invoiceStatusDTO.Name;

                var result = await _invoiceStatusRepository.UpdateAsync(existingInvoiceStatus);
                return result
                    ? new ServiceResponse<InvoiceStatusDTO>(InvoiceStatusFactory.ToDTO(existingInvoiceStatus), true, "Invoice status updated successfully.")
                    : new ServiceResponse<InvoiceStatusDTO>(null!, false, "Failed to update invoice status.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<InvoiceStatusDTO>(null!, false, $"Something went wrong: {e.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeleteInvoiceStatusAsync(int invoiceStatusId)
        {
            try
            {
                var existingInvoiceStatus = await _invoiceStatusRepository.GetAsync(i => i.Id == invoiceStatusId);
                if (existingInvoiceStatus == null)
                    return new ServiceResponse<bool>(false, false, "Invoice status not found.");

                var result = await _invoiceStatusRepository.RemoveAsync(existingInvoiceStatus);
                return result
                    ? new ServiceResponse<bool>(true, true, "Invoice status deleted successfully.")
                    : new ServiceResponse<bool>(false, false, "Failed to delete invoice status.");
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>(false, false, $"Something went wrong: {e.Message}");
            }
        }
    }
}
