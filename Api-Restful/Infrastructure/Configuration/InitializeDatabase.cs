using Api_Restful.Infrastructure.Migrations;

namespace Api_Restful.Infrastructure.Configuration
{
    public static class DatabaseInitializer // Renamed the class to avoid conflict with the method name
    {
        public static void Initialize(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DatabaseContext>();
            DbInitializer.Initialize(context);
        }
    }
}
