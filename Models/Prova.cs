namespace CrudTemplate.Models
{
    public class Prova
    {
        // Primary Key
        public int idProva { get; set; }
        public DateTime dataHora { get; set; }
        public Disciplina disciplina { get; set; }
        public TipoProva tipo { get; set; }

    }
} 