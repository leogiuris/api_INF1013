using ModelagemAPI.Data;
using ModelagemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelagemAPI.Controllers
{
    [Route("api/turmas")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurmaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Turmas
        // Pega todas as turmas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetTurmas()
        {
            return await _context.Turma.ToListAsync();
        }

        // GET: api/Turmas/5
        // Pega uma turma específica pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> GetTurma(int id)
        {
            var turma = await _context.Turma.FindAsync(id);

            if (turma == null)
            {
                return NotFound();
            }

            return turma;
        }

        // PUT: api/Turmas/5
        //  Atualiza uma turma específica usando o ID, e enviando um Json com os campos a serem atualizados.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurma(int id, Turma turma)
        // Recebe no request além do id da turma, um header com os campos em turmas a serem atualizados, e os valores.
        {
            if (id != turma.idTurma)
            {
                return BadRequest();
            }

            _context.Entry(turma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurmaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Turmas
        //  Cria uma nova turma enviando um Json com os dados da turma.
        [HttpPost]
        public async Task<ActionResult<Turma>> PostTurma(Turma turma)
        {
            _context.Turma.Add(turma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTurma", new { id = turma.idTurma }, turma);
        }

        // DELETE: api/Turmas/5
        // Deleta uma turma específica pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            var turma = await _context.Turma.FindAsync(id);
            if (turma == null)
            {
                return NotFound();
            }

            _context.Turma.Remove(turma);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Turmas/5/Alunos/1
        // Cria uma associação entre uma turma e um aluno, adicionando o aluno à turma.
        // O aluno deve ser previamente cadastrado.

        [HttpPost("{turmaId}/alunos/{alunoId}")]
        public async Task<IActionResult> AddAlunoToTurma(int turmaId, int alunoId)
        {
            var turma = await _context.Turma
                                    .Include(t => t.alunos)
                                    .FirstOrDefaultAsync(t => t.idTurma == turmaId);

            if (turma == null)
            {
                return NotFound("Turma não encontrada.");
            }

            var aluno = await _context.Aluno.FindAsync(alunoId);

            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            if (turma.alunos.Any(a => a.idAluno == alunoId))
            {
                return Conflict("Aluno já está associado a esta turma.");
            }

            turma.alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }   

        // Checa se uma turma existe pelo ID
        private bool TurmaExists(int id)
        {
            return _context.Turma.Any(e => e.idTurma == id);
        }
    }
} 