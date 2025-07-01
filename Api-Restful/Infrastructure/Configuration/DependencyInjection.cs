using TaskDisc.Application.Interfaces;
using TaskDisc.Application.Services;
using TaskDisc.Infrastructure.Repositories;
using TaskDisc.Infrastructure.Services;

namespace TaskDisc.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
            services.AddScoped<ITaskUserService, TaskUserManagementService>();
            services.AddScoped<ITaskUserRepository, TaskUserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskManagementService>();
            services.AddScoped<IUserService, UserManagementService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthManagementService>();
            services.AddScoped<ITokenRepository, TokenRepository>();


            return services;
        }
    }
}
