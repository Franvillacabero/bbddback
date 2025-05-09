using Back.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampoController : ControllerBase
    {
        private readonly ICampoService _campoService;

        public CampoController(ICampoService campoService)
        {
            _campoService = campoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var campos = await _campoService.GetAllAsync();
            return Ok(campos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var campo = await _campoService.GetByIdAsync(id);
            if (campo == null) return NotFound();
            return Ok(campo);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Campo campo)
        {
            await _campoService.AddAsync(campo);
            return CreatedAtAction(nameof(GetById), new { id = campo.Id }, campo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Campo campo)
        {
            if (id != campo.Id) return BadRequest();
            await _campoService.UpdateAsync(campo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _campoService.DeleteAsync(id);
            return NoContent();
        }
    }
}