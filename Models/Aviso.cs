
namespace ModelagemAPI.Models
{
    public class Aviso
    {
        // Primary Key
        public int idAviso { get; set; }
        public Aluno aluno { get; set; }
        public Prova prova { get; set; }
        public DateTime dataEnviado { get; set; }
        public DateTime dataAEnviar { get; set; }
        public string mensagem { get; set; }
        public Turma Turma { get; set; }
        public Disciplina disciplina { get; set; }
    }

} 