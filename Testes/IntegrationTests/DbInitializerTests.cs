
using Api_Restful.Core.Entities;
using Api_Restful.Infrastructure.Migrations;


namespace Api_Restful.Testes.IntegrationTests
{
    public class DbInitializerTests
    {
        [Fact]
        public void Initialize_ShouldPopulateDatabase_WhenDatabaseIsEmpty()
        {
            using var context = TestDatabaseContext.Create();
            Assert.Empty(context.JobTitles);

            DbInitializer.Initialize(context);

            Assert.Equal(3, context.JobTitles.Count());
            Assert.Equal(3, context.Tasks.Count());
            Assert.Equal(3, context.Users.Count());
            Assert.Equal(3, context.TaskUsers.Count());
            Assert.Equal(3, context.Tokens.Count());

            var jobTitles = context.JobTitles.ToList();
            Assert.Contains(jobTitles, jt => jt.Name == "Desenvolvedor" && jt.Abbreviation == "DEV");
            Assert.Contains(jobTitles, jt => jt.Name == "Analista" && jt.Abbreviation == "ANL");
            Assert.Contains(jobTitles, jt => jt.Name == "Gerente" && jt.Abbreviation == "GER");
        }

        [Fact]
        public void Initialize_ShouldNotPopulateDatabase_WhenDatabaseIsNotEmpty()
        {
            using var context = TestDatabaseContext.Create();
            context.JobTitles.Add(new JobTitles { ID_PK = 1, Name = "Existing", Abbreviation = "EXT" });
            context.SaveChanges();
            var initialCount = context.JobTitles.Count();

            DbInitializer.Initialize(context);

            Assert.Equal(initialCount, context.JobTitles.Count());
        }
    }
}