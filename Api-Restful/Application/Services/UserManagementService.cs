using Api_Restful.Application.Interfaces;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Services
{
    public class UserManagementService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserManagementService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(UserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto), "User data cannot be null.");
            if (string.IsNullOrEmpty(userDto.Name)) throw new ArgumentException("Nome é obrigatório.", nameof(userDto.Name));
            if (string.IsNullOrEmpty(userDto.Email)) throw new ArgumentException("Email é obrigatório.", nameof(userDto.Email));
            if (string.IsNullOrEmpty(userDto.Password)) throw new ArgumentException("Senha é obrigatória.", nameof(userDto.Password));
            if (userDto.ID_JobTitle <= 0) throw new ArgumentException("ID do cargo é obrigatório.", nameof(userDto.ID_JobTitle));

          
            if (!userDto.Email.Contains("@")) throw new ArgumentException("Email inválido.", nameof(userDto.Email));

            var user = new User
            {
                ID_PK = userDto.ID_PK,
                Name = userDto.Name,
                Password = userDto.Password,
                Email = userDto.Email,
                ID_JobTitle = userDto.ID_JobTitle ?? 1,
                ID_Token = userDto.ID_Token
            };

            return _userRepository.Add(user);
        }

        public bool DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return false;
            return _userRepository.Delete(id); 
        }

        public async  Task<UserDto> GetUserById(int id)
        {
            _userRepository.GetById(id);
            var user = _userRepository.GetById(id);
            if (user == null) throw new InvalidOperationException("User not found.");

            var userDto = new UserDto
            {
                ID_PK = user.ID_PK,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                ID_JobTitle = user.ID_JobTitle,
                ID_Token = user.ID_Token
            };

            return userDto;
        }

        public List<User> GetUsersByCargoId(int cargoId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateUser(UserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto), "User data cannot be null.");
            if (userDto.ID_PK <= 0) throw new ArgumentException("ID do usuário é obrigatório.", nameof(userDto.ID_PK));
            var existingUser = _userRepository.GetById(userDto.ID_PK);
            if (existingUser == null) throw new InvalidOperationException("Usuário não encontrado.");

           
            existingUser.Name = string.IsNullOrEmpty(userDto.Name) ? existingUser.Name : userDto.Name;
            existingUser.Email = string.IsNullOrEmpty(userDto.Email) ? existingUser.Email : userDto.Email;
            existingUser.Password = string.IsNullOrEmpty(userDto.Password) ? existingUser.Password : userDto.Password;
            existingUser.ID_JobTitle = (int)(userDto.ID_JobTitle > 0 ? userDto.ID_JobTitle : existingUser.ID_JobTitle);

            return _userRepository.Update(existingUser);
        }
    }
}
