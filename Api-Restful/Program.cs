using Api_Restful.Application.Mappings;
using Api_Restful.Infrastructure;
using Api_Restful.Infrastructure.Configuration;
using Api_Restful.Infrastructure.Middleware;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCustomMappings();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer <token>'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});




#region Discord Bot Configuration
builder.Services.AddHostedService<DiscordBotService>();
builder.Services.AddSingleton<DiscordSocketClient>();
builder.Services.AddSingleton<CommandService>();
#endregion

#region Database Configuration
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
DependencyInjection.AddApplicationServices(builder.Services);
#endregion

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

DatabaseInitializer.Initialize(app);

#region Swagger and Middleware Configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();