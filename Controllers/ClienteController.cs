using Microsoft.AspNetCore.Mvc;
using Models;
using Back.Services;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Cliente cannot be null.");
            }

            await _clienteService.AddAsync(cliente);
            return CreatedAtAction(nameof(GetById), new { id = cliente.Id_Cliente }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id_Cliente)
            {
                return BadRequest("ID mismatch.");
            }

            var existingCliente = await _clienteService.GetByIdAsync(id);
            if (existingCliente == null)
            {
                return NotFound();
            }

            await _clienteService.UpdateAsync(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingCliente = await _clienteService.GetByIdAsync(id);
            if (existingCliente == null)
            {
                return NotFound();
            }

            await _clienteService.DeleteAsync(id);
            return NoContent();
        }
    }
}