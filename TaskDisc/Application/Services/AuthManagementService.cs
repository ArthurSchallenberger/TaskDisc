using TaskDisc.Application.Interfaces;
using AutoMapper;

namespace TaskDisc.Application.Services
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
            var token = string.Empty;
            if (VerifyTokenValidaty(email).Result)
            {
                token = _jwtAuthenticationService.GenerateToken(email, password);
            }

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

        public async Task<string> RefreshToken(string token)
        {
            var tokenEntity = await _tokenRepository.GetByHashToken(token);
            if (tokenEntity == null || tokenEntity.IsRevoked)
            {
                throw new InvalidOperationException("Token not found or has been revoked.");
            }
       
            var newToken = _jwtAuthenticationService.GenerateToken(tokenEntity.User.Email, tokenEntity.User.Password);
            
            tokenEntity.IsRevoked = true; 
            _mapper.Map(tokenEntity, tokenEntity);

            await _tokenRepository.Update(tokenEntity);
            return newToken;
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

        //TODO CHECK IF THE USER HAS A VALID TOKEN
        private async Task<bool> VerifyTokenValidaty(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            var expiredTokens = await _tokenRepository.GetExpiredTokensByUserId(user.Id);
            var newExpiredTokens = expiredTokens.FirstOrDefault();

            if (newExpiredTokens.Expiration_Date < DateTime.UtcNow)
            {
                await RefreshToken(newExpiredTokens.Token);
                return true;
            }

            return false;
        }
    }
}
