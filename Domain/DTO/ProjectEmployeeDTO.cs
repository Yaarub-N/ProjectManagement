namespace Domain.DTO
{
    public class ProjectEmployeeDTO
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
    }
}
