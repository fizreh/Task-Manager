using TaskManager.AppLayer.DTOs;
using TaskManager.AppLayer.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Services;

public class TaskService : ITaskService
{
    private readonly List<TodoTask> _tasks = new();

    public Task<TodoTask> CreateTaskAsync(CreateTaskDTO taskDTO)
    {
        var task = new TodoTask(
            taskDTO.Title,
            taskDTO.Description,
            taskDTO.Priority,
            taskDTO.DueDate,
            taskDTO.AssignedToUserId,
            taskDTO.AssignedByUserId);

        _tasks.Add(task);

        return Task.FromResult(task);
    }

    public Task<List<TodoTask>> GetAllTasksAsync()
    {
        return Task.FromResult(_tasks.ToList());
    }

    public Task<TodoTask?> GetTaskByIdAsync(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task UpdateTaskAsync(TodoTask task)
    {
        var existing = _tasks.FirstOrDefault(t => t.Id == task.Id);

        if (existing is null)
            return Task.CompletedTask;

        _tasks.Remove(existing);
        _tasks.Add(task);

        return Task.CompletedTask;
    }

    public Task CompleteTaskAsync(Guid taskId)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);

        task?.MarkCompleted();

        return Task.CompletedTask;
    }

    public Task DeleteTaskAsync(Guid taskId)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);

        if (task is not null)
            _tasks.Remove(task);

        return Task.CompletedTask;
    }

    public Task AssignTaskAsync(Guid taskId, Guid assignedToUserId, Guid assignedByUserId)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);

        task?.ChangeAssignedTo(assignedToUserId, assignedByUserId);

        return Task.CompletedTask;
    }

    public Task ReopenTaskAsync(Guid taskId)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);

        task?.Reopen();

        return Task.CompletedTask;
    }

   
}