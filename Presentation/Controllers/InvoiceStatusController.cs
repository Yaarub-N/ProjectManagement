using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/invoicestatuses")]
    public class InvoiceStatusController(InvoiceStatusService invoiceStatusService) : ControllerBase
    {
        private readonly InvoiceStatusService _invoiceStatusService = invoiceStatusService;

        [HttpPost]
        public async Task<IActionResult> CreateInvoiceStatus([FromBody] InvoiceStatusDTO invoiceStatusDTO)
        {
            var response = await _invoiceStatusService.CreateInvoiceStatusAsync(invoiceStatusDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{invoiceStatusId}")]
        public async Task<IActionResult> GetInvoiceStatus(int invoiceStatusId)
        {
            var response = await _invoiceStatusService.GetInvoiceStatusByIdAsync(invoiceStatusId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{invoiceStatusId}")]
        public async Task<IActionResult> UpdateInvoiceStatus(int invoiceStatusId, [FromBody] InvoiceStatusDTO invoiceStatusDTO)
        {
            var response = await _invoiceStatusService.UpdateInvoiceStatusAsync(invoiceStatusId, invoiceStatusDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{invoiceStatusId}")]
        public async Task<IActionResult> DeleteInvoiceStatus(int invoiceStatusId)
        {
            var response = await _invoiceStatusService.DeleteInvoiceStatusAsync(invoiceStatusId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}