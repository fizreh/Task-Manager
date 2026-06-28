using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.AppLayer.Interfaces
{
    public interface ITaskService
    {
        public Task<TodoTask> CreateTaskAsync(
           string title, 
           string? description, 
           Priority priority, 
           DateTime dueDate, 
           Guid? assignedToUserId, 
           Guid? assignedByUserId);
        public Task UpdateTaskAsync(TodoTask task);
        public Task<TodoTask?> GetTaskByIdAsync(Guid taskId);
        public Task<List<TodoTask>> GetAllTasksAsync();


        public Task CompleteTaskAsync(Guid taskId);

        public Task DeleteTaskAsync(Guid taskId);

        public Task AssignTaskAsync(Guid taskId, Guid assignedToUserId, Guid assignedByUserId);
    }
}
