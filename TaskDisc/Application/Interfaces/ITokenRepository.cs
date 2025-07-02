
namespace TaskDisc.Application.Interfaces;

public interface ITokenRepository
{
    Task<TokenEntity> Add(TokenEntity tokenEntity);
    Task<TokenEntity> GetByHashToken(string hashToken);
    Task<TokenEntity> GetValidyHashTokenByEmail(string email);
    Task<ICollection<TokenEntity>> GetExpiredTokensByUserId(int id);
    Task<TokenEntity> GetValidyTokenByUserId(int id);
    Task<bool> TokenExists(string token);
    Task<TokenEntity> Update(TokenEntity tokenEntity);
    Task<bool> Delete(int id);
}
