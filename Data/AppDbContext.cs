using Microsoft.EntityFrameworkCore;
using ModelagemAPI.Models;

namespace ModelagemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Entity> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            });
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasKey(e => e.idAluno);
                entity.Property(e => e.nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.email).IsRequired().HasMaxLength(100);
            });
            modelBuilder.Entity<Turma>(entity =>
            {
                entity.HasKey(e => e.idTurma);
                entity.Property(e => e.nomeTurma).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.disciplina)
                      .WithMany()
                      .HasForeignKey("codDisciplina")
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<TipoProva>(entity =>
            {
                entity.HasKey(e => e.tipo);
                entity.Property(e => e.tipo).IsRequired().HasMaxLength(50);
            });
            modelBuilder.Entity<Disciplina>(entity =>
            {
                entity.HasKey(e => e.codDisciplina);
                entity.Property(e => e.nome).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.idSala);
                entity.Property(e => e.bloco).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Numero).IsRequired().HasMaxLength(10);
            });
        }
    }
} 