using ModelagemAPI.Data;
using ModelagemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelagemAPI.Controllers
{
    [Route("api/disciplinas")]
    [ApiController]
    public class DisciplinaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Disciplinas
        // Pega todas as disciplinas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disciplina>>> GetDisciplinas()
        {
            return await _context.Disciplina.ToListAsync();
        }

        // GET: api/Disciplinas/5
        // Pega uma disciplina específica pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Disciplina>> GetDisciplina(string id)
        {
            var disciplina = await _context.Disciplina.FindAsync(id);

            if (disciplina == null)
            {
                return NotFound();
            }

            return disciplina;
        }

        // PUT: api/Disciplinas/5
        // Atualiza uma disciplina específica usando o ID, e enviando um Json com os campos a serem atualizados.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisciplina(string id, Disciplina disciplina)
        {
            if (id != disciplina.codDisciplina)
            {
                return BadRequest();
            }

            _context.Entry(disciplina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisciplinaExists(id))
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

        // POST: api/Disciplinas
        // Cria uma nova disciplina enviando um Json com os dados da disciplina.
        [HttpPost]
        public async Task<ActionResult<Disciplina>> PostDisciplina(Disciplina disciplina)
        {
            _context.Disciplina.Add(disciplina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDisciplina", new { id = disciplina.codDisciplina }, disciplina);
        }

        // DELETE: api/Disciplinas/5
        // Deleta uma disciplina específica pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisciplina(string id)
        {
            var disciplina = await _context.Disciplina.FindAsync(id);
            if (disciplina == null)
            {
                return NotFound();
            }

            _context.Disciplina.Remove(disciplina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Verifica se uma disciplina existe pelo ID
        private bool DisciplinaExists(string id)
        {
            return _context.Disciplina.Any(e => e.codDisciplina == id);
        }
    }
} 