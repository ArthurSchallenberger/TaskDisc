using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Interfaces
{
    public interface ITaskUserService
    {
        Task<TaskUserDto> GetByUserId(int id);
        Task<TaskUserDto> GetByTaskId(int taskId);
        Task<TaskUserDto> Update(TaskUserDto taskUser);
        Task<TaskUserEntity> CreateTaskUser(TaskUserDto taskUserDto);
        Task<bool> DeleteTaskUser(int id);
    }
}
