using Api_Restful.Application.Interfaces;

namespace Api_Restful.Application.Services
{
    public class AuthManagementService : IAuthService
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthManagementService(
            IAuthService authService,
             IUserService userService
        )
        {
            _authService = authService;
            _userService = userService;
        }

        public Task<string> Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }
        //public async Task<string> Authenticate(string email, string password)
        //{
        //    if (!_userService.ValidateUserCredentials(email, password).Result)
        //    {
        //        throw new UnauthorizedAccessException("Invalid email or password.");
        //    }
        //    var token = _authService.Authenticate(email, password);


        //}
    }
}
