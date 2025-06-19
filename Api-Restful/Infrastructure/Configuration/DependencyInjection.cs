using Api_Restful.Application.Interfaces;
using Api_Restful.Application.Services;
using Api_Restful.Infrastructure.Repositories;
using Api_Restful.Infrastructure.Services;

namespace Api_Restful.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, JwtAuthenticationService>();
            services.AddScoped<ITaskUserService, TaskUserManagementService>();
            services.AddScoped<ITaskUserRepository, TaskUserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskManagementService>();
            services.AddScoped<IUserService, UserManagementService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
