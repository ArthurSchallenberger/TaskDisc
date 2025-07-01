using AutoMapper;
using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Dto;

namespace TaskDisc.Application.Mappings;

public class TokenMapping : Profile
{
    public TokenMapping()
    {
        CreateMap<TokenEntity, TokenDto>().ReverseMap();
    }
}
