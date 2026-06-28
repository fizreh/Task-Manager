using TaskManager.Domain.Constants;

namespace TaskManager.Domain.Validations;

public static class TaskValidator
{
   

    public static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title is required.", nameof(title));

        if (title.Length > ConstantAttributes.MaxTitleLength)
            throw new ArgumentException(
                $"Task title cannot exceed {ConstantAttributes.MaxTitleLength} characters.",
                nameof(title));
    }

    public static void ValidateDescription(string? description)
    {
        if (description is not null &&
            description.Length > ConstantAttributes.MaxDescriptionLength)
        {
            throw new ArgumentException(
                $"Task description cannot exceed {ConstantAttributes.MaxDescriptionLength} characters.",
                nameof(description));
        }
    }

    public static void ValidateDueDate(DateTime dueDate)
    {
        if (dueDate.Date < DateTime.Today)
            throw new ArgumentException(
                "Due date cannot be in the past.",
                nameof(dueDate));
    }

    public static void ValidateAssignment(Guid? assignedToUserId, Guid? assignedByUserId)
    {
        if (assignedToUserId.HasValue && !assignedByUserId.HasValue)
        {
            throw new ArgumentException(
                "AssignedByUserId is required when assigning a task.");
        }

        if (!assignedToUserId.HasValue && assignedByUserId.HasValue)
        {
            throw new ArgumentException(
                "AssignedByUserId cannot be specified when the task is unassigned.");
        }

        if (assignedToUserId.HasValue &&
            assignedByUserId.HasValue &&
            assignedToUserId.Value == assignedByUserId.Value)
        {
            throw new ArgumentException(
                "A user cannot assign a task to themselves.");
        }
    }
}
