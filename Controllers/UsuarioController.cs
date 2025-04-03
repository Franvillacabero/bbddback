using Microsoft.AspNetCore.Mvc;
using Models;
using Back.Repository;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Usuario no puede ser nulo.");
            }

            await _usuarioRepository.AddAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id_Usuario }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null || id != usuario.Id_Usuario)
            {
                return BadRequest("Usuario no puede ser nulo y el ID debe coincidir.");
            }

            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }

            await _usuarioRepository.UpdateAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            await _usuarioRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Contraseña))
            {
                return BadRequest("Usuario y contraseña son requeridos.");
            }

            var existingUsuario = await _usuarioRepository.GetByNameAndPasswordAsync(usuario.Nombre, usuario.Contraseña);
            if (existingUsuario == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            return Ok(existingUsuario);
        }
    }
}