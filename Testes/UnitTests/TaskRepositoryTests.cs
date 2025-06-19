using Microsoft.EntityFrameworkCore;
using Xunit;
using Api_Restful.Infrastructure.Repositories;
using Api_Restful.Core.Entities;

namespace Testes.UnitTests;
public class TaskRepositoryTests
{
    [Fact]
    public void GetAll_ShouldReturnTasks()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using (var context = new DatabaseContext(options))
        {
            context.Tasks.Add(new TaskEntity
            {
                Subject = "Tarefa de Teste",
                Description = "Descrição da tarefa",
                Status = "Pendente",
                Priority = 1,
                Creation_Date = DateTime.UtcNow
            });
            context.SaveChanges();
        }

        using (var context = new DatabaseContext(options))
        {
            var repo = new TaskRepository(context);
            var tasks = repo.GetAll();

            Assert.Single(tasks);
            Assert.Equal("Tarefa de Teste", tasks.First().Subject);
        }
    }
}
