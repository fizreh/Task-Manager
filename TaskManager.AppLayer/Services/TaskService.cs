using Microsoft.EntityFrameworkCore;
using TaskManager.AppLayer.DTOs;
using TaskManager.AppLayer.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Infrastructure.Persistence;


namespace TaskManager.Application.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;


    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TodoTask> CreateTaskAsync(CreateTaskDTO taskDTO)
    {
        var task = new TodoTask(
            taskDTO.Title,
            taskDTO.Description,
            taskDTO.Priority,
            taskDTO.DueDate,
            taskDTO.AssignedToUserId,
            taskDTO.AssignedByUserId);

       
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<List<TodoTask>> GetAllTasksAsync()
    {
        return await _context.Tasks
      .AsNoTracking()
      .ToListAsync();
    }

    public Task<TodoTask?> GetTaskByIdAsync(Guid id)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public async Task<TodoTask> UpdateTaskAsync(UpdateTaskDTO taskDTO)
    {
        var existing = _context.Tasks.FirstOrDefault(t => t.Id == taskDTO.Id);

        if (existing is null)
        {
            existing = await _context.Tasks.FindAsync(taskDTO.Id);
            if (existing is null)
            {
                throw new KeyNotFoundException($"Task with ID {taskDTO.Id} was not found.");
            }
        }
        existing.Rename(taskDTO.Title);
        existing.ChangeDescription(taskDTO.Description);
        existing.ChangePriority(taskDTO.Priority);
        existing.ChangeDueDate(taskDTO.DueDate);
        
        _context.Tasks.Update(existing);

        await _context.SaveChangesAsync();

        return existing;
    }

    public async Task CompleteTaskAsync(Guid taskId)
    {
        var task = await _context.Tasks.FindAsync(taskId);

        if (task == null) return;

        task.MarkCompleted();

        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(Guid taskId)
    {
        var task = await _context.Tasks.FindAsync(taskId);

        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public Task AssignTaskAsync(Guid taskId, Guid assignedToUserId, Guid assignedByUserId)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);

        task?.ChangeAssignedTo(assignedToUserId, assignedByUserId);

        return Task.CompletedTask;
    }

    public Task ReopenTaskAsync(Guid taskId)
    {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);

        task?.Reopen();

        return Task.CompletedTask;
    }

   
}