namespace Domain.DTO
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public int ProjectId { get; set; }
        public int InvoiceStatusId { get; set; }
        public int? CustomerId { get; set; }
    }
}
