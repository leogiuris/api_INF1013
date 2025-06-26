using ModelagemAPI.Data;
using ModelagemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelagemAPI.Controllers
{
    [Route("api/salas")]
    [ApiController]
    public class SalaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Salas
        // Pega todas as salas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSalas()
        {
            return await _context.Sala.ToListAsync();
        }

        // GET: api/Salas/5
        // Pega uma sala específica pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Sala>> GetSala(int id)
        {
            var sala = await _context.Sala.FindAsync(id);

            if (sala == null)
            {
                return NotFound();
            }

            return sala;
        }

        // PUT: api/Salas/5
        // Atualiza uma sala específica usando o ID, e enviando um Json com os campos a serem atualizados.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSala(int id, Sala sala)
        {
            if (id != sala.idSala)
            {
                return BadRequest();
            }

            _context.Entry(sala).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaExists(id))
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

        // POST: api/Salas
        // Cria uma nova sala enviando um Json com os dados da sala.
        [HttpPost]
        public async Task<ActionResult<Sala>> PostSala(Sala sala)
        {
            _context.Sala.Add(sala);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSala", new { id = sala.idSala }, sala);
        }

        // DELETE: api/Salas/5
        // Deleta uma sala específica pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var sala = await _context.Sala.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }

            _context.Sala.Remove(sala);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Checa se uma sala existe pelo ID
        private bool SalaExists(int id)
        {
            return _context.Sala.Any(e => e.idSala == id);
        }
    }
} 