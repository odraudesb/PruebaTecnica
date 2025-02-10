using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Prueba_Tecnica.Services;
using Prueba_Tencica_Core.Models;
using Serilog;

namespace Prueba_Tecnica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(UsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllUsuariosAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los usuarios.");
                return StatusCode(500, $"Error al obtener los usuarios: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuariosByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario.");
                return StatusCode(500, $"Error al obtener el usuario: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest();
                }

                await _usuarioService.AddUsuarioAsync(usuario);
                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario.");
                return StatusCode(500, $"Error al crear el usuario: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if (id != usuario.Id)
                {
                    return BadRequest();
                }

                await _usuarioService.UpdateUsuarioAsync(usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el usuario.");
                return StatusCode(500, $"Error al actualizar el usuario: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetUsuariosByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }

                await _usuarioService.DeleteUsuarioAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el usuario.");
                return StatusCode(500, $"Error al eliminar el usuario: {ex.Message}");
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Usuario login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.UsuarioNombre) || string.IsNullOrEmpty(login.Pass))
                {
                    return BadRequest("Usuario o contraseña no proporcionados.");
                }

                var usuario = await _usuarioService.LoginAsync(login.UsuarioNombre, login.Pass);
                if (usuario == null)
                {
                    return Unauthorized("Credenciales incorrectas");
                }

                var token = _usuarioService.GenerateJwtToken(usuario);

                return Ok(new { isSuccess = true, token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar iniciar sesión.");
                return StatusCode(500, $"Error al intentar iniciar sesión: {ex.Message}");
            }
        }
        [HttpGet("ValidarToken")]
        public IActionResult ValidarToken(string token)
        {
            try
            {
                var isValid = _usuarioService.ValidateJwtToken(token);

                if (isValid)
                {
                    return Ok(new { isSuccess = true });
                }
                else
                {
                    return Unauthorized(new { isSuccess = false });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar el token.");
                return StatusCode(500, $"Error al validar el token: {ex.Message}");
            }
        }

    }
}
