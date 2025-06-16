namespace Api_Restful.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task Add(Task task);
        Task GetById(int id);
        Task Update(Task task);
        bool Delete(int id);
    }
}
