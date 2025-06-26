using Microsoft.EntityFrameworkCore;
using ModelagemAPI.Models;

namespace ModelagemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Entity> Entity { get; set; }
        public DbSet<Disciplina> Disciplina { get; set; }
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<TipoProva> TipoProva { get; set; }
        public DbSet<Sala> Sala { get; set; }
        public DbSet<Prova> Prova { get; set; }
        public DbSet<Aviso> Aviso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            });
            modelBuilder.Entity<Disciplina>(entity =>
            {
                entity.HasKey(e => e.codDisciplina);
                entity.Property(e => e.nome).IsRequired().HasMaxLength(100);
                entity.HasMany(e => e.turmas)
                      .WithOne(t => t.disciplina_fk)
                      .HasForeignKey("idTurma")
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasKey(e => e.idAluno);
                entity.Property(e => e.nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.email).IsRequired().HasMaxLength(100);
                entity.HasMany(e => e.turmas)
                      .WithMany(t => t.alunos)
                      .UsingEntity(j => j.ToTable("AlunoTurma"));
                
            });
            modelBuilder.Entity<Turma>(entity =>
            {
                entity.HasKey(e => e.idTurma);
                entity.Property(e => e.nomeTurma).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.disciplina_fk)
                      .WithMany(d => d.turmas)
                      .HasForeignKey(t => t.CodDisciplinaFK)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.alunos)
                      .WithMany(a => a.turmas)
                      .UsingEntity(j => j.ToTable("AlunoTurma"));
                entity.HasMany(e => e.provas)
                      .WithOne(p => p.turma_fk)
                      .HasForeignKey("idTurma")
                      .OnDelete(DeleteBehavior.Cascade);
            });
            
            modelBuilder.Entity<TipoProva>(entity =>
            {
                entity.HasKey(e => e.tipo);
                entity.Property(e => e.tipo).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.idSala);
                entity.Property(e => e.bloco).IsRequired().HasMaxLength(50);
                entity.Property(e => e.numero).IsRequired().HasMaxLength(10);
                entity.HasMany(e => e.provas)
                      .WithOne(p => p.sala_fk)
                      .HasForeignKey("idSala")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.turmas)
                      .WithMany(t => t.salas)
                        .UsingEntity(j => j.ToTable("TurmaSala"));
                
            });

            modelBuilder.Entity<Prova>(entity =>
            {
                entity.HasKey(e => e.idProva);
                entity.Property(e => e.dataHora).IsRequired();
                entity.HasOne(e => e.disciplina_fk)
                      .WithMany()
                      .HasForeignKey(p => p.CodDisciplinaFK)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.tipo_fk)
                      .WithMany()
                      .HasForeignKey("tipo")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.sala_fk)
                      .WithMany()
                      .HasForeignKey(p => p.IdSalaFK)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Aviso>(entity =>
            {
                  // idAviso is automatically generated
                  
                  entity.HasKey(e => e.idAviso);
                  entity.Property(e => e.tipoAviso).IsRequired(); // 0 - Dia, 1 - Semana
                  entity.Property(e => e.dataEnviado).IsRequired();
                entity.Property(e => e.dataAEnviar).IsRequired();
                entity.Property(e => e.mensagem).IsRequired().HasMaxLength(500);
                entity.HasOne(e => e.aluno_fk)
                      .WithMany()
                      .HasForeignKey("idAluno")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.prova_fk)
                      .WithMany()
                      .HasForeignKey("idProva")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.turma_fk)
                      .WithMany()
                      .HasForeignKey("idTurma")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.disciplina_fk)
                      .WithMany()
                      .HasForeignKey("codDisciplina")
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
} 