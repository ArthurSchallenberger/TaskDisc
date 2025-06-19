using Api_Restful.Core.Entities;

namespace Api_Restful.Application.Interfaces
{
    public interface IUserRepository
    {
        User Add(User user);
        User GetById(int id);
        List<User> GetByCargoId(int cargoId);
        User Update(User user);
        bool Delete(int id);

        IEnumerable<User> GetAll();
    }
}
