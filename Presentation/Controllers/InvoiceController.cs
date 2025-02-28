using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoiceController(InvoiceService invoiceService) : ControllerBase
    {
        private readonly InvoiceService _invoiceService = invoiceService;

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDTO invoiceDTO)
        {
            var response = await _invoiceService.CreateInvoiceAsync(invoiceDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetInvoice(int invoiceId)
        {
            var response = await _invoiceService.GetInvoiceByIdAsync(invoiceId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{invoiceId}")]
        public async Task<IActionResult> UpdateInvoice(int invoiceId, [FromBody] InvoiceDTO invoiceDTO)
        {
            var response = await _invoiceService.UpdateInvoiceAsync(invoiceId, invoiceDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{invoiceId}")]
        public async Task<IActionResult> DeleteInvoice(int invoiceId)
        {
            var response = await _invoiceService.DeleteInvoiceAsync(invoiceId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}
