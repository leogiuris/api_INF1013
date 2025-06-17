namespace ModelagemAPI.Models
{
    public class Disciplina
    {
        // Primary Key
        public string codDisciplina { get; set; }
        public string nome { get; set; }

        public ICollection<Turma> turmas { get; set; }

    }
} 