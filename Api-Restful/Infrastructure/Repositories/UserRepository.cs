using Api_Restful.Application.Interfaces;
using Api_Restful.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Restful.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<UserEntity> Add(UserEntity user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<UserEntity> GetById(int id)
    {
        return await _context.Users
            .Include(u => u.JobTitle)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<UserEntity>> GetByJobTittleId(int idJobTittle)
    {
        return await _context.Users
            .Include(u => u.JobTitle)
            .Where(u => u.ID_JobTitle == idJobTittle).ToListAsync();
    }

    public async Task<UserEntity> Update(UserEntity user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> Delete(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return false;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<UserEntity>> GetAll()
    {
        return await _context.Users
            .Include(u => u.JobTitle)
            .ToListAsync();
    }

    public async Task<UserEntity> GetByEmail(string email)
    {
        return await _context.Users
            .Include(u => u.JobTitle)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}
