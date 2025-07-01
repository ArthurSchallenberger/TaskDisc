using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Dto;

namespace TaskDisc.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskEntity> CreateTask(TaskDto taskDto);
        Task<TaskDto> UpdateTask(TaskDto taskDto);
        Task<bool> DeleteTask(int id);
        Task<IEnumerable<TaskDto>> GetAllTasks();
        Task<TaskDto> GetTaskById(int id);
    }
}
