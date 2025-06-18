using Api_Restful.Application.Interfaces;
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

        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User GetById(int id)
        {
            return _context.Users.Include(u => u.JobTitle).FirstOrDefault(u => u.ID_PK == id);
        }

        public List<User> GetByCargoId(int cargoId)
        {
            return _context.Users.Include(u => u.JobTitle).Where(u => u.ID_JobTitle == cargoId).ToList();
        }

        public User Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.ID_PK == id);
            if (user == null) return false;
            _context.Users.Remove(user); 
            _context.SaveChanges();
            return true;
        }
    }
}
