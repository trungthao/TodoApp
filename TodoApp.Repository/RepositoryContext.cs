using Microsoft.EntityFrameworkCore;
using TodoApp.Entities.Models;
using TodoApp.Repository.Configuration;

namespace TodoApp.Repository;
public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ListTaskConfiguration());
        modelBuilder.ApplyConfiguration(new TaskItemConfiguration());
    }

    public DbSet<ListTask> ListTasks { get; set; }

    public DbSet<TaskItem> TaskItems { get; set; }
}

