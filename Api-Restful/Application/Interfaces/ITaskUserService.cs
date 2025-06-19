using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Interfaces
{
    public interface ITaskUserService
    {
        Task<TaskUser> GetByUserId(int id);
        Task<TaskUserDto> GetByTaskId(int taskId);
        Task<TaskUser> Update(TaskUserDto taskUser);
        Task<TaskUser> CreateTaskUser(TaskUserDto taskUserDto);
        bool DeleteTaskUser(int id);
    }
}
