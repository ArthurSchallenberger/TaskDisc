using Api_Restful.Core.Entities;

namespace Api_Restful.Application.Mappings;

public static class MappingRegistration
{
    public static IServiceCollection AddCustomMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(JobTitlesMapping).Assembly);
        services.AddAutoMapper(typeof(TaskUserMapping).Assembly);
        services.AddAutoMapper(typeof(TaskEntity).Assembly);
        services.AddAutoMapper(typeof(TaskUserMapping).Assembly);

        return services;
    }
}
