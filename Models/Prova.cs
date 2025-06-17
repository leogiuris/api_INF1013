namespace ModelagemAPI.Models
{
    public class Prova
    {
        // Primary Key
        public int idProva { get; set; }
        public DateTime dataHora { get; set; }
        public Disciplina disciplina_fk { get; set; }
        public TipoProva tipo_fk { get; set; }

        public Turma turma_fk { get; set; }
        public Sala sala_fk { get; set; }

    }
} 