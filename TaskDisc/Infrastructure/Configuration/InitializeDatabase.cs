using TaskDisc.Infrastructure.Migrations;

namespace TaskDisc.Infrastructure.Configuration
{
    public static class DatabaseInitializer
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
