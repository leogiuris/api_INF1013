using ModelagemAPI.Data;
using ModelagemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelagemAPI.Controllers
{
    [Route("api/alunos")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Alunos  (Pega os alunos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            return await _context.Aluno.ToListAsync();
        }

        // GET: api/Entities/5 (Pega um aluno específico)
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return aluno;
        }

        // PUT: api/Entities/5
        // Atualiza um aluno específico usando o ID, e enviando um Json com os campos a serem atualizados.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            if (id != aluno.idAluno)
            {
                return BadRequest();
            }

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
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

        // POST: api/Entities
        // Cria um novo aluno, enviando um Json com os dados do aluno.
        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
            _context.Aluno.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluno", new { id = aluno.idAluno }, aluno);
        }

        // DELETE: api/Entities/5
        // Deleta um aluno específico usando o ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollAlunoInTurmas([FromBody] EnrollmentRequest request)
        {
            var aluno = await _context.Aluno
                                      .Include(a => a.turmas)
                                      .FirstOrDefaultAsync(a => a.idAluno == request.IdAluno);

            if (aluno == null)
            {
                return NotFound($"Aluno with ID {request.IdAluno} not found.");
            }

            foreach (var idTurma in request.IdsTurma)
            {
                var turma = await _context.Turma.FindAsync(idTurma);
                if (turma == null)
                {
                    // Optionally, you can choose to skip or return an error for non-existent turmas
                    return NotFound($"Turma with ID {idTurma} not found.");
                }

                if (!aluno.turmas.Any(t => t.idTurma == idTurma))
                {
                    aluno.turmas.Add(turma);
                }
            }

            await _context.SaveChangesAsync();

            return Ok($"Aluno {aluno.nome} enrolled in specified turmas.");
        }

        // Checa se um aluno existe com o ID fornecido
        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.idAluno == id);
        }
    }
} 