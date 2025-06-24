namespace ModelagemAPI.Models
{
    public class EnrollmentRequest
    {
        public int IdAluno { get; set; }
        public List<int> IdsTurma { get; set; }
    }
} 