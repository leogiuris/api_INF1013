using Microsoft.EntityFrameworkCore;

namespace ModelagemAPI.Services
{
    public class AlunoService
    {
        private readonly Data.AppDbContext _context;

        public AlunoService(Data.AppDbContext context)
        {
            _context = context;
        }

        // Define um método assíncrono que retorna uma lista de alunos (IEnumerable<Aluno>) associada a uma determinada prova.
        public async Task<IEnumerable<Models.Aluno>> GetAlunosByProvaId(int idProva)
        {
            var prova = await _context.Prova
                                      .Include(p => p.turma_fk)
                                          .ThenInclude(t => t.alunos)
                                      .FirstOrDefaultAsync(p => p.idProva == idProva);

            if (prova == null || prova.turma_fk == null)
            {
                return null;
            }
            return prova.turma_fk.alunos;
        }
    }
} 