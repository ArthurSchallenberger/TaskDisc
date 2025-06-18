using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Interfaces
{
    public interface ITaskService
    {
        Task<Task> CreateTask(TaskDto taskDto);
        Task GetTaskById(int id);
        List<Task> GetTasksByUserId(int userId);
        Task<Task> UpdateTask(TaskDto taskDto);
        bool DeleteTask(int id);
    }
}
