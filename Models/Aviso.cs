
namespace ModelagemAPI.Models
{
    public class Aviso
    {
        // Primary Key
        public int idAviso { get; set; }
        public Aluno aluno_fk { get; set; }
        public Prova prova_fk { get; set; }
        public DateTime dataEnviado { get; set; }
        public DateTime dataAEnviar { get; set; }
        public string mensagem { get; set; }
        public Turma turma_fk { get; set; }
        public Disciplina disciplina_fk { get; set; }
    }

} 