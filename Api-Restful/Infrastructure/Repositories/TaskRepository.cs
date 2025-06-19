using Api_Restful.Application.Interfaces;
using Api_Restful.Core.Entities;

namespace Api_Restful.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DatabaseContext _context;

        public TaskRepository(DatabaseContext context)
        {
            _context = context;
        }

        public TaskEntity Add(TaskEntity task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public TaskEntity Update(TaskEntity task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
            return task;
        }

        public bool Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return false;
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return true;
        }


        #region Gets
        public IEnumerable<TaskEntity> GetAll()
        {
            return _context.Tasks.ToList();
        }

        public TaskEntity GetById(int id)
        {
            return _context.Tasks.Find(id);
        }
        #endregion
    }
}