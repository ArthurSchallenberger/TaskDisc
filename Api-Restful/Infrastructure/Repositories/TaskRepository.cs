using Api_Restful.Application.Interfaces;

namespace Api_Restful.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DatabaseContext _context;

        public TaskRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Task Add(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public Task GetById(int id)
        {
            return _context.Tasks.Find(id);
        }

    

        public Task Update(Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
            return task;
        }

        public bool Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return false;
            _context.SaveChanges();
            return true;
        }
    }
}
