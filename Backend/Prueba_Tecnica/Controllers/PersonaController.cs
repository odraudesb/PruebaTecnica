using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica.Services;
using Prueba_Tencica_Core.Models;

namespace Prueba_Tecnica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly PersonaService _personaService;
        private readonly ILogger<PersonaController> _logger;
        public PersonaController(PersonaService personaService, ILogger<PersonaController> logger)
        {
            _personaService = personaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetAll()
        {
            try
            {
                var personas = await _personaService.GetAllPersonasAsync();
                return Ok(personas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las personas.");
                return StatusCode(500, $"Error al obtener las personas: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetById(int id)
        {
            try
            {
                var persona = await _personaService.GetPersonaByIdAsync(id);
                if (persona == null)
                {
                    return NotFound();
                }
                return Ok(persona);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la persona.");
                return StatusCode(500, $"Error al obtener la persona: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> Create([FromBody] Persona persona)
        {
            try
            {
                if (persona == null)
                {
                    return BadRequest();
                }

                await _personaService.AddPersonaAsync(persona);
                return CreatedAtAction(nameof(GetById), new { id = persona.Id }, persona);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la persona.");
                return StatusCode(500, $"Error al crear la persona: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Persona persona)
        {
            try
            {
                if (id != persona.Id)
                {
                    return BadRequest();
                }

                await _personaService.UpdatePersonaAsync(persona);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la persona.");
                return StatusCode(500, $"Error al actualizar la persona: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var persona = await _personaService.GetPersonaByIdAsync(id);
                if (persona == null)
                {
                    return NotFound();
                }

                await _personaService.DeletePersonaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la persona.");
                return StatusCode(500, $"Error al eliminar la persona: {ex.Message}");
            }
        }
    }
}
