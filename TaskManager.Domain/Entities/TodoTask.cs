using TaskManager.Domain.Enums;
using TaskManager.Domain.Validations;
using TaskStatus = TaskManager.Domain.Enums.TaskStatus;

namespace TaskManager.Domain.Entities;

public class TodoTask
{
    public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public Priority Priority { get; private set; }

    public TaskStatus Status { get; private set; }

    public Guid? AssignedToUserId { get; private set; }

    public Guid? AssignedByUserId { get; private set; }

    public DateTime DueDate { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public DateTime? UpdatedOn { get; private set; }

    public DateTime? CompletedOn { get; private set; }

    #region Constructor

    public TodoTask(
        string title,
        string? description,
        Priority priority,
        DateTime dueDate,
        Guid? assignedToUserId = null,
        Guid? assignedByUserId = null)
    {
        Id = Guid.NewGuid();
        CreatedOn = DateTime.UtcNow;

        TaskValidator.ValidateTitle(title);
        TaskValidator.ValidateDescription(description);
        TaskValidator.ValidateDueDate(dueDate);
        TaskValidator.ValidateAssignment(assignedToUserId, assignedByUserId);

        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;

        AssignedToUserId = assignedToUserId;
        AssignedByUserId = assignedByUserId;

        Status = TaskStatus.Pending;
    }

    #endregion

    #region Behavior Methods

    public void Rename(string title)
    {
        TaskValidator.ValidateTitle(title);

        Title = title;
        Touch();
    }

    public void ChangeDescription(string? description)
    {
        TaskValidator.ValidateDescription(description);

        Description = description;
        Touch();
    }

    public void ChangePriority(Priority priority)
    {
        Priority = priority;
        Touch();
    }

    public void ChangeDueDate(DateTime dueDate)
    {
        TaskValidator.ValidateDueDate(dueDate);

        DueDate = dueDate;
        Touch();
    }

    public void ChangeAssignedTo(Guid? assignedToUserId, Guid? assignedByUserId)
    {
        TaskValidator.ValidateAssignment(assignedToUserId, assignedByUserId);

        AssignedToUserId = assignedToUserId;
        AssignedByUserId = assignedByUserId;

        Touch();
    }

    public void MarkCompleted()
    {
        if (Status == TaskStatus.Completed)
            return;

        Status = TaskStatus.Completed;
        CompletedOn = DateTime.UtcNow;

        Touch();
    }

    public void Reopen()
    {
        if (Status != TaskStatus.Completed)
            return;

        Status = TaskStatus.Pending;
        CompletedOn = null;

        Touch();
    }

    public void Update(
        string title,
        string? description,
        Priority priority,
        DateTime dueDate)
    {
        Rename(title);
        ChangeDescription(description);
        ChangePriority(priority);
        ChangeDueDate(dueDate);
    }

    #endregion

    #region Internal Helpers

    private void Touch()
    {
        UpdatedOn = DateTime.UtcNow;
    }

    #endregion
}