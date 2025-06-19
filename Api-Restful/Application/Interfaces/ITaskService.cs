using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Interfaces
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
