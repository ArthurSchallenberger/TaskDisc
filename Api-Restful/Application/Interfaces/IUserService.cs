using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserEntity> CreateUser(UserDto userDto);
        Task<UserDto> GetUserById(int id);
        List<UserEntity> GetUsersByCargoId(int cargoId);
        Task<UserEntity> UpdateUser(UserDto userDto);
        bool DeleteUser(int id);

        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
