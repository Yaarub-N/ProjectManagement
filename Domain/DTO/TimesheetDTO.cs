namespace Domain.DTO
{
    public class TimesheetDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }
}
