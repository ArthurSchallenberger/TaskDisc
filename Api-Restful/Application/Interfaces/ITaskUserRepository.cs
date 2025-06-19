using Api_Restful.Core.Entities;

namespace Api_Restful.Application.Interfaces
{
    public interface ITaskUserRepository
    {
        TaskUser Add(TaskUser taskUser);
        TaskUser GetByUserId(int id);
        TaskUser GetByTaskId(int taskId);
        TaskUser Update(TaskUser taskUser);
        bool Delete(int id);
    }
}
