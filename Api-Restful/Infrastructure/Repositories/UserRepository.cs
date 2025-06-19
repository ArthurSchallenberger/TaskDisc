using Api_Restful.Application.Interfaces;
using Api_Restful.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Restful.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public UserEntity Add(UserEntity user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public UserEntity GetById(int id)
        {
            return _context.Users.Include(u => u.JobTitle).FirstOrDefault(u => u.Id == id);
        }

        public List<UserEntity> GetByCargoId(int cargoId)
        {
            return _context.Users.Include(u => u.JobTitle).Where(u => u.ID_JobTitle == cargoId).ToList();
        }

        public UserEntity Update(UserEntity user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<UserEntity> GetAll()
        {
            return _context.Users.Include(u => u.JobTitle).ToList();
        }
    }
}
