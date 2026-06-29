
using TaskManager.Domain.Enums;

namespace TaskManager.AppLayer.DTOs
{
    public class CreateTaskDTO
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Priority Priority { get; set; }

        public DateTime DueDate { get; set; }

        public Guid? AssignedToUserId { get; set; }

        public Guid? AssignedByUserId { get; set; }
    }
}
