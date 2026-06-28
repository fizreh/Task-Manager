using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Events;

public class TaskCreatedEvent : DomainEvent
{
    public TodoTask Task { get; }

    public TaskCreatedEvent(TodoTask task)
    {
        Task = task;
    }
}