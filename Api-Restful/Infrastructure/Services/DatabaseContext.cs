using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class DatabaseContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<UsuarioTarefa> UsuarioTarefas { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Token> Tokens { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsuarioTarefa>()
            .HasKey(ut => new { ut.ID_Usuario, ut.ID_Tarefa });

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.UsuarioTarefas)
            .WithOne(ut => ut.Usuario)
            .HasForeignKey(ut => ut.ID_Usuario);

        modelBuilder.Entity<Tarefa>()
            .HasMany(t => t.UsuarioTarefas)
            .WithOne(ut => ut.Tarefa)
            .HasForeignKey(ut => ut.ID_Tarefa);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Cargo)
            .WithMany(c => c.Usuarios)
            .HasForeignKey(u => u.ID_Cargo);

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Tokens)
            .WithOne(t => t.Usuario)
            .HasForeignKey(t => t.ID_Usuario);
    }
}