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
                new JobTitlesEntity { Name = "Desenvolvedor", Abbreviation = "DEV" },
                new JobTitlesEntity { Name = "Analista", Abbreviation = "ANL" },
                new JobTitlesEntity { Name = "Gerente", Abbreviation = "GER" },
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
                new UserEntity
                {
                    Name = "Alice",
                    Email = "alice@email.com",
                    Password = "Senha123!",
                    ID_JobTitle = jobTitles[0].Id
                },
                new UserEntity
                {
                    Name = "Bob",
                    Email = "bob@email.com",
                    Password = "Senha123!",
                    ID_JobTitle = jobTitles[1].Id
                },
                new UserEntity
                {
                    Name = "Carol",
                    Email = "carol@email.com",
                    Password = "Senha123!",
                    ID_JobTitle = jobTitles[2].Id
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            // TaskUsers
            var taskUsers = new[]
            {
                new TaskUserEntity { Id_Task = tasks[0].Id, Id_User = users[0].Id },
                new TaskUserEntity { Id_Task = tasks[1].Id, Id_User = users[1].Id },
                new TaskUserEntity { Id_Task = tasks[2].Id, Id_User = users[2].Id },
            };
            context.TaskUsers.AddRange(taskUsers);

            // Tokens
            var tokens = new[]
            {
                new TokenEntity { Creation_Date = DateTime.UtcNow, ID_User = users[0].Id },
                new TokenEntity { Creation_Date = DateTime.UtcNow, ID_User = users[1].Id },
                new TokenEntity { Creation_Date = DateTime.UtcNow, ID_User = users[2].Id },
            };
            context.Tokens.AddRange(tokens);

            context.SaveChanges();
        }
    }
}
