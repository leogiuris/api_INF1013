namespace ModelagemAPI.Models
{
    public class Turma
    {
        // Primary Key
        public int idTurma { get; set; }
        public string nomeTurma { get; set; }
        public Disciplina disciplina_fk { get; set; }

    }
} 