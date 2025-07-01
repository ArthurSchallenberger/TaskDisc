
namespace Api_Restful.Application.Interfaces;

public interface ITokenRepository
{
    Task<TokenEntity> Add(TokenEntity tokenEntity);
    Task<TokenEntity> GetByHashToken(string hashToken);
    Task<ICollection<TokenEntity>> GetExpiredTokensByUserId(int id);
    Task<TokenEntity> Update(TokenEntity tokenEntity);
    Task<bool> Delete(int id);
}
