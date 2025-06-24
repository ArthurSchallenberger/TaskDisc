using Api_Restful.Core.Entities;

namespace Api_Restful.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> Add(UserEntity user);
        Task<UserEntity> GetById(int id);
        Task<IEnumerable<UserEntity>> GetByJobTittleId(int cargoId);
        Task<UserEntity> Update(UserEntity user);
        Task<bool> Delete(int id);

        Task<IEnumerable<UserEntity>> GetAll();
    }
}
