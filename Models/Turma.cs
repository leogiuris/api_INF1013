namespace ModelagemAPI.Models
{
    public class Turma
    {
        // Primary Key
        public int idTurma { get; set; }
        public string nomeTurma { get; set; }
        public string CodDisciplinaFK { get; set; }
        public Disciplina disciplina_fk { get; set; }
        public ICollection<Aluno> alunos { get; set; }

        public ICollection<Prova> provas { get; set; }

        public ICollection<Sala> salas { get; set; }

    }
} 