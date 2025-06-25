using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Interfaces
{
    public interface ITokenRepository
    {

        Task<TokenEntity> Add(TokenEntity tokenEntity);
        Task<TokenEntity> Update(TokenEntity tokenEntity);
        Task<bool> Delete(int id);
    }
}
