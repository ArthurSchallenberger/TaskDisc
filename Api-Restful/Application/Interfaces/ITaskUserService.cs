using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Interfaces
{
    public interface ITaskUserService
    {
        Task<TaskUserEntity> GetByUserId(int id);
        Task<TaskUserDto> GetByTaskId(int taskId);
        Task<TaskUserEntity> Update(TaskUserDto taskUser);
        Task<TaskUserEntity> CreateTaskUser(TaskUserDto taskUserDto);
        bool DeleteTaskUser(int id);
    }
}
