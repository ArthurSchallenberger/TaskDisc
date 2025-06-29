using Api_Restful.Application.Interfaces;
using AutoMapper;

namespace Api_Restful.Application.Services
{
    public class AuthManagementService : IAuthService
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;
        public AuthManagementService(
            IJwtAuthenticationService jwtAuthenticationService,
             IUserService userService,
             IUserRepository userRepository,
             ITokenRepository tokenRepository,
             IMapper mapper
        )
        {
            _jwtAuthenticationService = jwtAuthenticationService;
            _userService = userService;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _mapper = mapper;
        }


        public async Task<string> AuthenticateToken(string email, string password)
        {
            if (!_userService.ValidateUserCredentials(email, password).Result)
            {
                throw new UnauthorizedAccessException("invalid email or password.");
            }
            var token = _jwtAuthenticationService.GenerateToken(email, password);

            return await RegisterTokenInUser(token, email) ?  token : throw new InvalidOperationException("Failed to register token for user.");
        }

        public async Task<bool> ValidateToken(string token)
        {
           var getTokenToValidate = _tokenRepository.GetByHashToken(token);
            if (getTokenToValidate is null)
            {
                throw new InvalidOperationException("Token not found.");
            }
            return _jwtAuthenticationService.ValidateToken(token);
        }

        private async Task<bool> RegisterTokenInUser(string token, string email)
        {
            var user = await _userRepository.GetByEmail(email);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var tokenEntity = new TokenEntity
            {
                Token = token,
                ID_User = user.Id,
                IsRevoked = false,
                Creation_Date = DateTime.UtcNow,
                Expiration_Date = DateTime.UtcNow.AddHours(1)
            };

            var inputToken = await _tokenRepository.Add(tokenEntity);
            
            return inputToken is not null;
        }
    }
}
