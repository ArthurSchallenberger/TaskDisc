using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Dto;

namespace TaskDisc.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserEntity> CreateUser(UserDto userDto);
        Task<UserDto> GetUserById(int id);
        Task<IEnumerable<UserDto>> GetAllUsersByJobTittleId(int jobTittleId);
        Task<bool> ValidateUserCredentials(string email, string password);
        Task<UserDto> UpdateUser(UserDto userDto);
        Task<bool> DeleteUser(int id);

        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
