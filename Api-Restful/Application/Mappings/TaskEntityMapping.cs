using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Dto;
using AutoMapper;

namespace TaskDisc.Application.Mappings;
public class TaskEntityMapping : Profile
{
    public TaskEntityMapping()
    {
        CreateMap<TaskEntity, TaskDto>()
            .ReverseMap()
            .ForAllMembers(opt => opt.Condition((dto, entity, aux) => aux != null));
    }
}
