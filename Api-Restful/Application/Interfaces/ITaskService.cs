using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskEntity> CreateTask(TaskDto taskDto);
        Task<TaskEntity> UpdateTask(TaskDto taskDto);
        bool DeleteTask(int id);
        IEnumerable<TaskEntity> GetAllTasks();
    }
}
