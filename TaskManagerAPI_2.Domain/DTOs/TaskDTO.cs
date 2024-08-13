namespace TaskManagerAPI_2.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        public string? Priority { get; set; }
        public DateOnly? DueDate { get; set; }
    }
}
