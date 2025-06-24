using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserEntity> CreateUser(UserDto userDto);
        Task<UserDto> GetUserById(int id);
        Task<IEnumerable<UserDto>> GetAllUsersByJobTittleId(int jobTittleId);
        Task<UserDto> UpdateUser(UserDto userDto);
        Task<bool> DeleteUser(int id);

        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
