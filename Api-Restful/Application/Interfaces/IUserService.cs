using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(UserDto userDto);
        Task<UserDto> GetUserById(int id);
        List<User> GetUsersByCargoId(int cargoId);
        Task<User> UpdateUser(UserDto userDto);
        bool DeleteUser(int id);

        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
