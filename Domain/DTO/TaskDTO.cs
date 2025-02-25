namespace Domain.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public int ProjectId { get; set; }
    }
}
