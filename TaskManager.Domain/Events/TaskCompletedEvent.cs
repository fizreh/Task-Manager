using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Events;

public class TaskCompletedEvent : DomainEvent
{
    public Guid TaskId { get; }

    public TaskCompletedEvent(Guid taskId)
    {
        TaskId = taskId;
    }
}