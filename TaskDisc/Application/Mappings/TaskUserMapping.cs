using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Dto;
using AutoMapper;

namespace TaskDisc.Application.Mappings;

public class TaskUserMapping : Profile
{
    public TaskUserMapping()
    {
        CreateMap<TaskUserEntity, TaskUserDto>()
            .ReverseMap()
            .ForAllMembers(opt => opt.Condition((dto, entity, aux) => aux != null));
    }
}
