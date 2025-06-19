using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;
using AutoMapper;

namespace Api_Restful.Application.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<UserEntity, UserDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ReverseMap();
    }
}
