using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TodoApp.Repository;

namespace TodoApp.Api.ContextFactory
{
	public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
	{
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(configuration.GetConnectionString("SqlConnection"), b => b.MigrationsAssembly("TodoApp.Api"));

            return new RepositoryContext(builder.Options);
        }
    }
}

