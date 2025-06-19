using System;
using System.Linq;
using Api_Restful.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Restful.Infrastructure.Migrations
{
    public static class DbInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            if (context.Database.IsRelational())
            {
                context.Database.Migrate();
            }

            if (context.JobTitles.Any())
                return;

            // JobTitles
            var jobTitles = new[]
            {
                new JobTitles { Name = "Desenvolvedor", Abbreviation = "DEV" },
                new JobTitles { Name = "Analista", Abbreviation = "ANL" },
                new JobTitles { Name = "Gerente", Abbreviation = "GER" },
            };
            context.JobTitles.AddRange(jobTitles);
            context.SaveChanges();

            // Tasks
            var tasks = new[]
            {
                new TaskEntity
                {
                    Subject = "Implementar autenticação",
                    Description = "Criar autenticação com JWT",
                    Status = "Pendente",
                    Priority = 1,
                    Creation_Date = DateTime.UtcNow
                },
                new TaskEntity
                {
                    Subject = "Criar testes",
                    Description = "Adicionar cobertura de testes",
                    Status = "Pendente",
                    Priority = 2,
                    Creation_Date = DateTime.UtcNow
                },
                new TaskEntity
                {
                    Subject = "Documentar API",
                    Description = "Escrever Swagger e README",
                    Status = "Pendente",
                    Priority = 3,
                    Creation_Date = DateTime.UtcNow
                }
            };
            context.Tasks.AddRange(tasks);
            context.SaveChanges();

            // Users
            var users = new[]
            {
                new User
                {
                    Name = "Alice",
                    Email = "alice@email.com",
                    Password = "Senha123!",
                    ID_JobTitle = jobTitles[0].ID_PK
                },
                new User
                {
                    Name = "Bob",
                    Email = "bob@email.com",
                    Password = "Senha123!",
                    ID_JobTitle = jobTitles[1].ID_PK
                },
                new User
                {
                    Name = "Carol",
                    Email = "carol@email.com",
                    Password = "Senha123!",
                    ID_JobTitle = jobTitles[2].ID_PK
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            // TaskUsers
            var taskUsers = new[]
            {
                new TaskUser { ID_Task = tasks[0].ID_PK, ID_User = users[0].ID_PK },
                new TaskUser { ID_Task = tasks[1].ID_PK, ID_User = users[1].ID_PK },
                new TaskUser { ID_Task = tasks[2].ID_PK, ID_User = users[2].ID_PK },
            };
            context.TaskUsers.AddRange(taskUsers);

            // Tokens
            var tokens = new[]
            {
                new Token { Creation_Date = DateTime.UtcNow, ID_User = users[0].ID_PK },
                new Token { Creation_Date = DateTime.UtcNow, ID_User = users[1].ID_PK },
                new Token { Creation_Date = DateTime.UtcNow, ID_User = users[2].ID_PK },
            };
            context.Tokens.AddRange(tokens);

            context.SaveChanges();
        }
    }
}
