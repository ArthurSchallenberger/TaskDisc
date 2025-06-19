using Api_Restful.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class DatabaseContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TaskUserEntity> TaskUsers { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<JobTitlesEntity> JobTitles { get; set; }
    public DbSet<TokenEntity> Tokens { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskUserEntity>()
            .HasKey(ut => new { ut.Id_User, ut.Id_Task });

        modelBuilder.Entity<TaskUserEntity>()
                .HasOne(tu => tu.User)
                .WithMany(u => u.TaskUsers)
                .HasForeignKey(tu => tu.Id_User);

        modelBuilder.Entity<TaskUserEntity>()
            .HasOne(tu => tu.Task)
            .WithMany(t => t.TaskUsers) 
            .HasForeignKey(tu => tu.Id_Task);


        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.TaskUsers)
            .WithOne(ut => ut.User)
            .HasForeignKey(ut => ut.Id_User);

        modelBuilder.Entity<TaskEntity>()
            .HasMany(t => t.TaskUsers)
            .WithOne(ut => ut.Task)
            .HasForeignKey(ut => ut.Id_Task);

        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.JobTitle)
            .WithMany(c => c.User)
            .HasForeignKey(u => u.ID_JobTitle);

        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Tokens)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.ID_User);
    }
}