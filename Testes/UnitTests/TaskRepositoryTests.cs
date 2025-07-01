using Microsoft.EntityFrameworkCore;
using TaskDisc.Infrastructure.Repositories;
using TaskDisc.Core.Entities;

namespace Testes.UnitTests
{
    public class TaskRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnTasks()
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
                await context.SaveChangesAsync();
            }

            using (var context = new DatabaseContext(options))
            {
                var repo = new TaskRepository(context);
                var tasks = await repo.GetAll();

                Assert.Single(tasks);
                Assert.Equal("Tarefa de Teste", tasks.First().Subject);
            }
        }
    }
}
