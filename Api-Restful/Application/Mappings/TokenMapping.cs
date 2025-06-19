using AutoMapper;
using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;

namespace Api_Restful.Application.Mappings;

public class TokenMapping : Profile
{
    public TokenMapping()
    {
        CreateMap<TokenEntity, TokenDto>().ReverseMap();
    }
}
