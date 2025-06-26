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
            var prova = await _context.Prova // Contexto: Tabela prova
                                      .Include(p => p.turma_fk) // Inclui a turma associada à prova
                                          .ThenInclude(t => t.alunos)  // Inclui os alunos associados à turma
                                      .FirstOrDefaultAsync(p => p.idProva == idProva); // busca a primeira prova cujo idProva bate com o parâmetro, ou null se não houver.

            // Resultado da busca: Uma prova com seus alunos, navegando pelas relações: Prova → Turma → Alunos.


            if (prova == null || prova.turma_fk == null)
            {
                return null;
            }
            return prova.turma_fk.alunos;
        }
    }
} 