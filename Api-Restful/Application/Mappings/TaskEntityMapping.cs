using Api_Restful.Core.Entities;
using Api_Restful.Presentation.Dto;
using AutoMapper;

namespace Api_Restful.Application.Mappings;
public class TaskEntityMapping : Profile
{
    public TaskEntityMapping()
    {
        CreateMap<TaskEntity, TaskDto>()
            .ReverseMap()
            .ForAllMembers(opt => opt.Condition((dto, entity, aux) => aux != null));
    }
}
