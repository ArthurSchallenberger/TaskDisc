using Api_Restful.Core.Entities;

namespace Api_Restful.Application.Interfaces
{
    public interface IUserRepository
    {
        UserEntity Add(UserEntity user);
        UserEntity GetById(int id);
        List<UserEntity> GetByCargoId(int cargoId);
        UserEntity Update(UserEntity user);
        bool Delete(int id);

        IEnumerable<UserEntity> GetAll();
    }
}
