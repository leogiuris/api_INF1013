namespace ModelagemAPI.Models
{
    public class Sala
    {
        // Primary Key
        public int idSala { get; set; }
        public string bloco { get; set; } // Kennedy, Leme, etc...
        public string numero { get; set; }

        public ICollection<Prova> provas { get; set; }

        public ICollection<Turma> turmas { get; set; }

    }
} 