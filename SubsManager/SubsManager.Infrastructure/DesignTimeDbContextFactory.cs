using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SubsManager.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SubsDbContext>
    {
        public SubsDbContext CreateDbContext(string[] args)
        {
            var all = Environment.GetEnvironmentVariables();
            foreach (System.Collections.DictionaryEntry entry in all)
            {
                Console.WriteLine($"{entry.Key} = {entry.Value}");
            }
            var cs = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRINGS") ?? throw new InvalidOperationException("Connection string 'Postgres' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<SubsDbContext>();

            optionsBuilder.UseNpgsql(cs, o => o.MigrationsAssembly(typeof(SubsDbContext).Assembly.FullName));

            return new SubsDbContext(optionsBuilder.Options);
        }
    }
}
