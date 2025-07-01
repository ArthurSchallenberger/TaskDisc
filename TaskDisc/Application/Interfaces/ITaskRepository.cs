using TaskDisc.Core.Entities;

namespace TaskDisc.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity> Add(TaskEntity task);
        Task<TaskEntity> GetById(int id);
        Task<TaskEntity> Update(TaskEntity task);
        Task<bool> Delete(int id);
        Task<IEnumerable<TaskEntity>> GetAll();
    }
}
