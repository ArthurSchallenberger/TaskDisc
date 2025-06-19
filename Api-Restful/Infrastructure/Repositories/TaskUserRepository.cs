using Api_Restful.Application.Interfaces;
using Api_Restful.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Restful.Infrastructure.Repositories
{
    public class TaskUserRepository : ITaskUserRepository
    {
        private readonly DatabaseContext _context;

        public TaskUserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public TaskUserEntity Add(TaskUserEntity taskUser)
        {
            var user = _context.Users.Find(taskUser.Id_User);
            if (user == null)
                throw new ArgumentException("User with the specified ID_Usuario does not exist.");
            _context.TaskUsers.Add(taskUser);
            _context.SaveChanges();
            return taskUser;
        }

        public TaskUserEntity GetByUserId(int id)
        {
            return _context.TaskUsers
                .Include(t => t.User)
                .Include(t => t.Task)
                .FirstOrDefault(t => t.Id_User == id);
        }

        public TaskUserEntity GetByTaskId(int taskId)
        {
            return _context.TaskUsers
                .Include(t => t.User)
                .Include(t => t.Task)
                .FirstOrDefault(t => t.Id_Task == taskId);
        }

        public TaskUserEntity Update(TaskUserEntity taskUser)
        {
            var user = _context.Users.Find(taskUser.Id_User);
            if (user == null)
                throw new ArgumentException("User with the specified ID_Usuario does not exist.");

            _context.TaskUsers.Update(taskUser);
            _context.SaveChanges();
            return taskUser;
        }

        public bool Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return false;
            task.Status = "Deleted";
            _context.SaveChanges();
            return true;
        }
    }
}
