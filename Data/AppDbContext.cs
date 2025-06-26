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

            // Define a entidade Entity
            // A Entity tem uma chave primária Id, um nome de no max 255 caracteres, e obrigatório.
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
                      .WithOne(t => t.disciplina_fk) //Possui muitas turmas, cada turma com uma disciplina. (Relaação N:1)
                      .HasForeignKey("idTurma") //  // Define a chave estrangeira idTurma (De turma) na disciplina.
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Define a entidade Aluno
            // O Aluno tem uma chave primária idAluno, um nome de no max 100 caracteres, um email de no max 100 caracteres, ambos obrigatórios.
            // O Aluno possui muitas turmas, cada turma com muitos alunos. (Relação N:M)
            // A relação N:M é feita através de uma tabela de junção chamada AlunoTurma.
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasKey(e => e.idAluno);

                entity.Property(e => e.nome).IsRequired().HasMaxLength(100);

                entity.Property(e => e.email).IsRequired().HasMaxLength(100);

                entity.HasMany(e => e.turmas)
                      .WithMany(t => t.alunos) // Possui muitas turmas, cada turma com muitos alunos. (Relação N:M)
                      .UsingEntity(j => j.ToTable("AlunoTurma")); // Define a relação explicitada acima para a tabela de junção entre Aluno e Turma.
                
            });

            // Define a entidade Turma
            // A Turma tem uma chave primária idTurma, um nomeTurma de no max 100 caracteres, e obrigatório.
            modelBuilder.Entity<Turma>(entity =>
            {
                entity.HasKey(e => e.idTurma);

                entity.Property(e => e.nomeTurma).IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.disciplina_fk)
                      .WithMany(d => d.turmas) // Possuí uma disciplina, cada disciplina com muitas turmas. (Relação 1:N)
                      .HasForeignKey(t => t.CodDisciplinaFK) // Define a chave estrangeira CodDisciplinaFK (De disciplina) na Turma.
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.alunos)
                      .WithMany(a => a.turmas) // Possui muitos alunos, cada aluno com muitas turmas. (Relação N:M)
                      .UsingEntity(j => j.ToTable("AlunoTurma")); // Define a relação explicitada acima para a tabela de junção entre Aluno e Turma.
                                                                  
                entity.HasMany(e => e.provas)
                      .WithOne(p => p.turma_fk) // Possui muitas provas, cada uma com uma turma. (Relação N:1)
                      .HasForeignKey("idTurma") // Define a chave estrangeira idTurma (De turma) na relação com Prova.
                      .OnDelete(DeleteBehavior.Cascade);
            });
            
            // Define a entidade TipoProva
            // O TipoProva tem uma chave primária tipo, que é uma string de no max 50 caracteres, e obrigatória.
            modelBuilder.Entity<TipoProva>(entity =>
            {
                entity.HasKey(e => e.tipo);
                entity.Property(e => e.tipo).IsRequired().HasMaxLength(50);
            });

            // Define a entidade Sala
            // A Sala tem uma chave primária idSala, um bloco de no max 50 caracteres, um número de no max 10 caracteres, ambos obrigatórios.
            // A Sala possui muitas provas, cada prova com uma sala. (Relação N:1)
            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.idSala);

                entity.Property(e => e.bloco).IsRequired().HasMaxLength(50);

                entity.Property(e => e.numero).IsRequired().HasMaxLength(10);

                entity.HasMany(e => e.provas)
                      .WithOne(p => p.sala_fk) // Possui muitas provas, cada prova com uma sala. (Relação N:1)
                      .HasForeignKey("idSala") // Define a chave estrangeira idSala (De sala) na relação com Prova.
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.turmas)
                      .WithMany(t => t.salas) // Possui muitas turmas, cada turma com muitas salas. (Relação N:M)
                        .UsingEntity(j => j.ToTable("TurmaSala")); // Define a relação explicitada acima para a tabela de junção entre Turma e Sala.
                
            });

            // Define a entidade Prova
            // A Prova tem uma chave primária idProva, e uma dataHora obrigatória.
            modelBuilder.Entity<Prova>(entity =>
            {
                entity.HasKey(e => e.idProva);

                entity.Property(e => e.dataHora).IsRequired();

                entity.HasOne(e => e.disciplina_fk)
                      .WithMany() // Possui uma disciplina, cada disciplina com muitas provas. (Relação 1:N)
                      .HasForeignKey(p => p.CodDisciplinaFK) // Define a chave estrangeira CodDisciplinaFK (De disciplina) na Prova.
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.tipo_fk)
                      .WithMany() // Possui um tipo de prova, cada tipo com muitas provas. (Relação 1:N)
                      .HasForeignKey("tipo") // Define a chave estrangeira tipo (De TipoProva) na Prova.
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.sala_fk)
                      .WithMany()  // Possui uma sala, cada sala com muitas provas. (Relação 1:N)
                      .HasForeignKey(p => p.IdSalaFK)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Define a entidade Aviso
            // O Aviso tem uma chave primária idAviso, um tipoAviso obrigatório (0 - Dia, 1 - Semana), 
            // uma Data Enviada obrigatória, uma Data a enviar obrigatória, e uma mensagem obrigatória de no max 500 caracteres.
            modelBuilder.Entity<Aviso>(entity =>
            {
                  // idAviso is automatically generated
                  
                  entity.HasKey(e => e.idAviso);

                  entity.Property(e => e.tipoAviso).IsRequired(); // 0 - Dia, 1 - Semana

                  entity.Property(e => e.dataEnviado).IsRequired();

                  entity.Property(e => e.dataAEnviar).IsRequired();

                  entity.Property(e => e.mensagem).IsRequired().HasMaxLength(500);

                  entity.HasOne(e => e.aluno_fk)
                      .WithMany() // Possui um aluno, cada aluno com muitos avisos. (Relação 1:N)
                      .HasForeignKey("idAluno") // Define a chave estrangeira idAluno (De Aluno) na relação com Aviso.
                      .OnDelete(DeleteBehavior.Cascade);

                  entity.HasOne(e => e.prova_fk)
                      .WithMany() // Possuí uma prova, cada prova com muitos avisos. (Relação 1:N)
                      .HasForeignKey("idProva") // Define a chave estrangeira idProva (De Prova) na relação com Aviso.
                      .OnDelete(DeleteBehavior.Cascade);

                  entity.HasOne(e => e.turma_fk)
                      .WithMany() // Possui uma turma, cada turma com muitos avisos. (Relação 1:N)
                      .HasForeignKey("idTurma") // Define a chave estrangeira idTurma (De Turma) na relação com Aviso.
                      .OnDelete(DeleteBehavior.Cascade);

                  entity.HasOne(e => e.disciplina_fk)
                      .WithMany()  // Possui uma disciplina, cada disciplina com muitos avisos. (Relação 1:N)
                      .HasForeignKey("codDisciplina") // Define a chave estrangeira codDisciplina (De Disciplina) na relação com Aviso.
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
} 