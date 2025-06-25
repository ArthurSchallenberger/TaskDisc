using Api_Restful.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_Restful.Infrastructure.Services
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly string _key;
        public JwtAuthenticationService(
            IConfiguration configuration
        )
        {
            _key = configuration["Jwt:Key"];
        }

        public  string GenerateToken(string email, string password)
        {
            var keyBytes = Encoding.ASCII.GetBytes(_key);
            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.NameIdentifier, email) 
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        string IJwtAuthenticationService.GenerateToken(string userId, string role)
        {
            throw new NotImplementedException();
        }
    }
}
