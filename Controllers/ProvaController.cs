using ModelagemAPI.Data;
using ModelagemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelagemAPI.Controllers
{
    [Route("api/provas")]
    [ApiController]
    public class ProvaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProvaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Provas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prova>>> GetProvas()
        {
            return await _context.Prova.ToListAsync();
        }

        // GET: api/Provas/5
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

        // PUT: api/Provas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prova>> PostProva(Prova prova)
        {
            _context.Prova.Add(prova);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProva", new { id = prova.idProva }, prova);
        }

        // DELETE: api/Provas/5
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

        private bool ProvaExists(int id)
        {
            return _context.Prova.Any(e => e.idProva == id);
        }
    }
} 