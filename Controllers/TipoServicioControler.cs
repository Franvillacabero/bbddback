using Microsoft.AspNetCore.Mvc;
using Models;
using Back.Services;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoServicioController : ControllerBase
    {
        private readonly ITipoServicioService _tipoServicioService;

        public TipoServicioController(ITipoServicioService tipoServicioService)
        {
            _tipoServicioService = tipoServicioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tipoServicios = await _tipoServicioService.GetAllAsync();
            return Ok(tipoServicios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tipoServicio = await _tipoServicioService.GetByIdAsync(id);
            if (tipoServicio == null)
            {
                return NotFound();
            }
            return Ok(tipoServicio);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TipoServicio tipoServicio)
        {
            if (tipoServicio == null)
            {
                return BadRequest("TipoServicio cannot be null.");
            }

            await _tipoServicioService.AddAsync(tipoServicio);
            return CreatedAtAction(nameof(GetById), new { id = tipoServicio.Id_TipoServicio }, tipoServicio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TipoServicio tipoServicio)
        {
            if (id != tipoServicio.Id_TipoServicio)
            {
                return BadRequest("ID mismatch.");
            }

            var existingTipoServicio = await _tipoServicioService.GetByIdAsync(id);
            if (existingTipoServicio == null)
            {
                return NotFound();
            }

            await _tipoServicioService.UpdateAsync(tipoServicio);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTipoServicio = await _tipoServicioService.GetByIdAsync(id);
            if (existingTipoServicio == null)
            {
                return NotFound();
            }

            await _tipoServicioService.DeleteAsync(id);
            return NoContent();
        }
    }
}