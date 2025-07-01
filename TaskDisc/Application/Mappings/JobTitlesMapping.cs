using AutoMapper;
using TaskDisc.Core.Entities;
using TaskDisc.Presentation.Dto;

namespace TaskDisc.Application.Mappings;

public class JobTitlesMapping : Profile
{
    public JobTitlesMapping()
    {
        CreateMap<JobTitlesEntity, JobTitlesDto>().ReverseMap();
    }
}