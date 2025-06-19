using Api_Restful.Application.Interfaces;
using Api_Restful.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Restful.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DatabaseContext _context;

        public TaskRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<TaskEntity> Add(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskEntity> Update(TaskEntity task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        #region Gets
        public async Task<IEnumerable<TaskEntity>> GetAll()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskEntity?> GetById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }
        #endregion
    }
}
