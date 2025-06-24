using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;
using AutoMapper;

namespace Api_Restful.Application.Mappings;

public class TaskUserMapping : Profile
{
    public TaskUserMapping()
    {
        CreateMap<TaskUserEntity, TaskUserDto>()
            .ReverseMap()
            .ForAllMembers(opt => opt.Condition((dto, entity, aux) => aux != null));
    }
}
