using Api_Restful.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api_Restful.Testes.IntegrationTests
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