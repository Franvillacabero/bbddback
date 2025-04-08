using Microsoft.AspNetCore.Mvc;
using Models;
using Back.Repository;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class RegistroController : ControllerBase
    {
        private readonly IRegistroRepository _registroRepository;

        public RegistroController(IRegistroRepository registroRepository)
        {
            _registroRepository = registroRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var registros = await _registroRepository.GetAllAsync();
            return Ok(registros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var registro = await _registroRepository.GetByIdAsync(id);
            if (registro == null)
            {
                return NotFound();
            }
            return Ok(registro);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Registro registro)
        {
            if (registro == null)
            {
                return BadRequest("Registro cannot be null.");
            }

            await _registroRepository.AddAsync(registro);
            return CreatedAtAction(nameof(GetById), new { id = registro.Id_Registro }, registro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Registro registro)
        {
            if (id != registro.Id_Registro)
            {
                return BadRequest("ID mismatch.");
            }

            var existingRegistro = await _registroRepository.GetByIdAsync(id);
            if (existingRegistro == null)
            {
                return NotFound();
            }

            await _registroRepository.UpdateAsync(registro);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registro = await _registroRepository.GetByIdAsync(id);
            if (registro == null)
            {
                return NotFound();
            }

            await _registroRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("tipoServicio/{Id_TipoServicio}")]
        public async Task<IActionResult> GetByTipoServicioId(int Id_TipoServicio)
        {
            var registros = await _registroRepository.GetByTipoServicioIdAsync(Id_TipoServicio);
            return Ok(registros);
        }

        [HttpGet("cliente/{Id_Cliente}")]
        public async Task<IActionResult> GetByClienteId(int Id_Cliente)
        {
            var registros = await _registroRepository.GetByClienteIdAsync(Id_Cliente);
            return Ok(registros);
        }

        [HttpGet("password/{id}")]
        public async Task<IActionResult> GetDecryptedPassword(int id)
        {
            var password = await _registroRepository.GetDecryptedPasswordAsync(id);
            
            if (password == null)
                return NotFound();

            return Ok(new { password });
        }
    }
}