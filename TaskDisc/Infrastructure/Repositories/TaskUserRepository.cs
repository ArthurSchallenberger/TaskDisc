using TaskDisc.Application.Interfaces;
using TaskDisc.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskDisc.Infrastructure.Repositories;

public class TaskUserRepository : ITaskUserRepository
{
    private readonly DatabaseContext _context;

    public TaskUserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<TaskUserEntity> Add(TaskUserEntity taskUser)
    {
        _context.TaskUsers.Add(taskUser);
        await _context.SaveChangesAsync();
        return taskUser;
    }
    public async Task<TaskUserEntity> GetById(int id)
    {
        return await _context.TaskUsers
            .Include(t => t.User)
            .Include(t => t.Task)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
    public async Task<TaskUserEntity> GetByUserId(int id)
    {
        return await _context.TaskUsers
            .Include(t => t.User)
            .Include(t => t.Task)
            .FirstOrDefaultAsync(t => t.Id_User == id);
    }

    public async Task<TaskUserEntity> GetByTaskId(int taskId)
    {
        return await _context.TaskUsers
            .Include(t => t.User)
            .Include(t => t.Task)
            .FirstOrDefaultAsync(t => t.Id_Task == taskId);
    }

    public async Task<TaskUserEntity> Update(TaskUserEntity taskUser)
    {
        var user = _context.Users.Find(taskUser.Id_User);
        if (user == null)
            throw new ArgumentException("User with the specified ID_Usuario does not exist.");

        _context.TaskUsers.Update(taskUser);
        await _context.SaveChangesAsync();
        return taskUser;
    }

    public async Task<bool> Delete(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task == null) return false;
        task.Status = "Deleted";
        await _context.SaveChangesAsync();
        return true;
    }
}
