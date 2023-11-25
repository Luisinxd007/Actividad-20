using ApiWeb3C.Server.Data;
using ApiWeb3C.Shared.Modelos;
using ApiWeb3C.Shared.Modelos.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb3C.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly BasedeDatosContext _context;

        public PersonasController(BasedeDatosContext context)
        {
            _context = context;
        }

        // GET: api/Personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaDTO>>> GetPersonas()
        {
            if (_context.Personas == null)
            {
                return NotFound();
            }
            var respuesta = _context.Personas.Include(c => c.Habitos).Select(r => new PersonaDTO() { Id = r.Nickname, Nombre = r.Posicion, Correo = r.Correo, Telefono = r.Nacionalidad, Clasificacion = r.Clasificacion.Nombre });
            return await respuesta.ToListAsync();
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jugador>> GetPersona(int id)
        {
            if (_context.Personas == null)
            {
                return NotFound();
            }
            var persona = await _context.Personas.Include(c => c.Clasificacion).Include(h => h.Habitos).FirstAsync(r => r.Nickname == id);

            if (persona == null)
            {
                return NotFound();
            }

            return persona;
        }

        // PUT: api/Personas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, Jugador persona)
        {
            if (id != persona.Nickname)
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
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

        // POST: api/Personas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jugador>> PostPersona(Jugador persona)
        {
            List<Habito> habitostemporal = new();
            if (persona.Habitos != null)
            {
                habitostemporal = persona.Habitos.ToList();
                persona.Habitos = null;
            }
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
            if (habitostemporal != null)
            {
                persona.Habitos = habitostemporal;
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetPersona", new { id = persona.Nickname }, persona);
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            if (_context.Personas == null)
            {
                return NotFound();
            }
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonaExists(int id)
        {
            return (_context.Personas?.Any(e => e.Nickname == id)).GetValueOrDefault();
        }
    }
}
