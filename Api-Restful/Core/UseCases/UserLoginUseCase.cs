using Api_Restful.Application.Interfaces;

namespace Api_Restful.Core.UseCases
{
    public class UserLoginUseCase : IUserService
    {
        private readonly IAuthenticationService _authService;

        public UserLoginUseCase(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public string Login(string username, string password)
        {
         
            return _authService.GenerateToken("userId", "user");
        }
    }
}
