using TaskManager.AppLayer.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.AppLayer.Interfaces
{
    public interface ITaskService
    {
        public Task<TodoTask> CreateTaskAsync(
           CreateTaskDTO taskDTO);
        public Task UpdateTaskAsync(TodoTask task);
        public Task<TodoTask?> GetTaskByIdAsync(Guid taskId);
        public Task<List<TodoTask>> GetAllTasksAsync();


        public Task CompleteTaskAsync(Guid taskId);

        public Task DeleteTaskAsync(Guid taskId);


        Task ReopenTaskAsync(Guid taskId);

        public Task AssignTaskAsync(Guid taskId, Guid assignedToUserId, Guid assignedByUserId);
    }
}
