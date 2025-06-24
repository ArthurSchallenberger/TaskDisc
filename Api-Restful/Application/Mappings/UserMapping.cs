using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;
using AutoMapper;

namespace Api_Restful.Application.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<UserEntity, UserDto>()
              .ReverseMap()
              .ForMember(entity => entity.ID_JobTitle, opt => opt.MapFrom(
                  (dto, entity) => dto.ID_JobTitle.HasValue ? dto.ID_JobTitle : entity.ID_JobTitle)
              ).ForAllMembers(opt => opt.Condition((dto, entity, aux) => aux != null));

    }
}
