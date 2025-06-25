using Api_Restful.Application.Interfaces;

namespace Api_Restful.Core.UseCases
{
    public class UserLoginUseCase 
    {
        private readonly IJwtAuthenticationService _authService;

        public UserLoginUseCase(IJwtAuthenticationService authService)
        {
            _authService = authService;
        }

        public string Login(string username, string password)
        {
         
            return _authService.GenerateToken("userId", "user");
        }
    }
}
