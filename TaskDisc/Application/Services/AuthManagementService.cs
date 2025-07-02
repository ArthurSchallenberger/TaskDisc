using TaskDisc.Application.Interfaces;

namespace TaskDisc.Application.Services
{
    public class AuthManagementService : IAuthService
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        
        public AuthManagementService(
            IJwtAuthenticationService jwtAuthenticationService,
            IUserService userService,
            IUserRepository userRepository,
            ITokenRepository tokenRepository)
        {
            _jwtAuthenticationService = jwtAuthenticationService;
            _userService = userService;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> AuthenticateToken(string email, string password)
        {
            if (!await _userService.ValidateUserCredentials(email, password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            string token;
            if (await VerifyTokenValidaty(email))
            {
                token = await GenerateUniqueToken(email, password);
            }
            else
            {
                var validToken = await _tokenRepository.GetValidyHashTokenByEmail(email);
                token = validToken?.Token ?? await GenerateUniqueToken(email, password);
            }

            await RegisterTokenInUser(token, email);
            return token;
        }

        private async Task<string> GenerateUniqueToken(string email, string password)
        {
            string token;
            bool isUnique;
            do
            {
                token = _jwtAuthenticationService.GenerateToken(email, password);
                isUnique = !await _tokenRepository.TokenExists(token);
            } while (!isUnique);

            return token;
        }

        public async Task<bool> ValidateToken(string token)
        {
            var tokenEntity = await _tokenRepository.GetByHashToken(token);
            if (tokenEntity == null)
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

            tokenEntity.IsRevoked = true;
            await _tokenRepository.Update(tokenEntity);

            return await GenerateUniqueToken(tokenEntity.User.Email, tokenEntity.User.Password);
        }

        private async Task<bool> RegisterTokenInUser(string token, string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (await _tokenRepository.TokenExists(token))
            {
                return false;
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
            return inputToken != null;
        }

        private async Task<bool> VerifyTokenValidaty(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var validToken = await _tokenRepository.GetValidyTokenByUserId(user.Id);
            if (validToken != null && !validToken.IsRevoked && validToken.Expiration_Date > DateTime.UtcNow)
            {
                await UpdateTokenUsage(validToken);
                return false; 
            }

            var expiredTokens = await _tokenRepository.GetExpiredTokensByUserId(user.Id);
            var expiredToken = expiredTokens.FirstOrDefault();
            if (expiredToken != null && expiredToken.Expiration_Date < DateTime.UtcNow && !expiredToken.IsRevoked)
            {
                await RefreshToken(expiredToken.Token);
                return true; 
            }

            return true;
        }

        private async Task UpdateTokenUsage(TokenEntity tokenEntity)
        {
            if (tokenEntity == null)
            {
                return; 
            }

            tokenEntity.LastUsed = DateTime.UtcNow;
            tokenEntity.Expiration_Date = DateTime.UtcNow.AddHours(1);
            
            tokenEntity.Token = _jwtAuthenticationService.GenerateToken(tokenEntity.User.Email, tokenEntity.User.Password);

            await _tokenRepository.Update(tokenEntity);
        }
    }
}