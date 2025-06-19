
using Api_Restful.Core.Entities;

namespace Api_Restful.Application.Interfaces
{
    public interface ITaskRepository
    {
        TaskEntity Add(TaskEntity task);
        TaskEntity GetById(int id);
        TaskEntity Update(TaskEntity task);
        bool Delete(int id);
        IEnumerable<TaskEntity> GetAll();
    }
}
