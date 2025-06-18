using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskUser> TaskUsers { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<JobTitles> JobTitles { get; set; }
    public DbSet<Token> Tokens { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskUser>()
            .HasKey(ut => new { ut.ID_User, ut.ID_Task });

        modelBuilder.Entity<TaskUser>()
                .HasOne(tu => tu.User)
                .WithMany(u => u.TaskUsers)
                .HasForeignKey(tu => tu.ID_User);

        modelBuilder.Entity<TaskUser>()
            .HasOne(tu => tu.Task)
            .WithMany(t => t.TaskUsers) 
            .HasForeignKey(tu => tu.ID_Task);


        modelBuilder.Entity<User>()
            .HasMany(u => u.TaskUsers)
            .WithOne(ut => ut.User)
            .HasForeignKey(ut => ut.ID_User);

        modelBuilder.Entity<Task>()
            .HasMany(t => t.TaskUsers)
            .WithOne(ut => ut.Task)
            .HasForeignKey(ut => ut.ID_Task);

        modelBuilder.Entity<User>()
            .HasOne(u => u.JobTitle)
            .WithMany(c => c.User)
            .HasForeignKey(u => u.ID_JobTitle);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Tokens)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.ID_User);
    }
}