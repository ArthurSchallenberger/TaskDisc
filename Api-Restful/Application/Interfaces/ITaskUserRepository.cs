using Api_Restful.Core.Entities;

namespace Api_Restful.Application.Interfaces
{
    public interface ITaskUserRepository
    {
        Task<TaskUserEntity> Add(TaskUserEntity taskUser);
        Task<TaskUserEntity> GetById(int id);
        Task<TaskUserEntity> GetByUserId(int id);
        Task<TaskUserEntity> GetByTaskId(int taskId);
        Task<TaskUserEntity> Update(TaskUserEntity taskUser);
        Task<bool> Delete(int id);
    }
}
