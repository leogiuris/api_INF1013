using ModelagemAPI.Data;
using ModelagemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelagemAPI.Services;

namespace ModelagemAPI.Controllers
{
    [Route("api/provas")]
    [ApiController]
    public class ProvaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AlunoService _alunoService;

        public ProvaController(AppDbContext context, AlunoService alunoService)
        {
            _context = context;
            _alunoService = alunoService;
        }

        // GET: api/Provas
        // Pega todas as provas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prova>>> GetProvas()
        {
            return await _context.Prova.ToListAsync();
        }

        // GET: api/Provas/5
        // Pega uma prova específica pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Prova>> GetProva(int id)
        {
            var prova = await _context.Prova.FindAsync(id);

            if (prova == null)
            {
                return NotFound();
            }

            return prova;
        }

        // Pega os alunos associados a uma prova específica
        [HttpGet("{id}/alunos")]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunosByProva(int id)
        {
            var alunos = await _alunoService.GetAlunosByProvaId(id);
            if (alunos == null)
            {
                return NotFound($"Prova with ID {id} not found.");
            }
            if (!alunos.Any())
            {
                return NotFound($"No students found for Prova with ID {id} or associated Turma.");
            }
            return Ok(alunos);
        }

        // PUT: api/Provas/5
        // Atualiza uma prova específica usando o ID, e enviando um Json com os campos a serem atualizados.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProva(int id, Prova prova)
        {
            if (id != prova.idProva)
            {
                return BadRequest();
            }

            _context.Entry(prova).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvaExists(id))
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

        // POST: api/Provas
        // Cria uma nova prova enviando um Json com os dados da prova.
        [HttpPost]
        public async Task<ActionResult<Prova>> PostProva(Prova prova)
        {
            _context.Prova.Add(prova);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProva", new { id = prova.idProva }, prova);
        }

        // DELETE: api/Provas/5
        // Deleta uma prova específica pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProva(int id)
        {
            var prova = await _context.Prova.FindAsync(id);
            if (prova == null)
            {
                return NotFound();
            }

            _context.Prova.Remove(prova);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Checa se uma prova existe pelo ID
        private bool ProvaExists(int id)
        {
            return _context.Prova.Any(e => e.idProva == id);
        }
    }
} 