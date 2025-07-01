using TaskDisc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace TaskDisc.Testes.IntegrationTests
{
    public class TestDatabaseContext : DatabaseContext
    {
        public TestDatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public static TestDatabaseContext Create()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new TestDatabaseContext(options);
        }
    }
}