using TaskDisc.Application.Interfaces;
using TaskDisc.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskDisc.Infrastructure.Repositories;

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
        var query =
            from User in _context.Users
            join jobTitle in _context.JobTitles on User.ID_JobTitle equals jobTitle.Id
            select new UserEntity
            {
                Id = User.Id,
                Name = User.Name,
                Email = User.Email,
                ID_JobTitle = User.ID_JobTitle,
                JobTitle = jobTitle
            };

        return await query.ToListAsync();
    }

    
    public async Task<IEnumerable<UserEntity>> GetAllUserIdAndName()
    {
        var query =
            from User in _context.Users
            select new UserEntity
            {
                Id = User.Id,
                Name = User.Name,
            };

        return await query.ToListAsync();
    }

    public async Task<UserEntity> GetByEmail(string email)
    {
        return await _context.Users
            .Include(u => u.JobTitle)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}
