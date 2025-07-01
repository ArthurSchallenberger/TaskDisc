using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Dto;

namespace TaskDisc.Application.Interfaces
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
