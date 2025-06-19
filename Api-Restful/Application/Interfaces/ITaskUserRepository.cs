using Api_Restful.Core.Entities;

namespace Api_Restful.Application.Interfaces
{
    public interface ITaskUserRepository
    {
        TaskUserEntity Add(TaskUserEntity taskUser);
        TaskUserEntity GetByUserId(int id);
        TaskUserEntity GetByTaskId(int taskId);
        TaskUserEntity Update(TaskUserEntity taskUser);
        bool Delete(int id);
    }
}
