using Models;
using Back.Repository;

namespace Back.Services
{
    public class RegistroService : IRegistroService
    {
        private readonly IRegistroRepository _registroRepository;

        public RegistroService(IRegistroRepository registroRepository)
        {
            _registroRepository = registroRepository;
        }

        public async Task<List<Registro>> GetAllAsync()
        {
            return await _registroRepository.GetAllAsync();
        }

        public async Task<Registro?> GetByIdAsync(int id)
        {
            return await _registroRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Registro registro)
        {
            await _registroRepository.AddAsync(registro);
        }

        public async Task UpdateAsync(Registro registro)
        {
            await _registroRepository.UpdateAsync(registro);
        }

        public async Task DeleteAsync(int id)
        {
            await _registroRepository.DeleteAsync(id);
        }

        public async Task<List<Registro>> GetByTipoServicioIdAsync(int Id_TipoServicio)
        {
            return await _registroRepository.GetByTipoServicioIdAsync(Id_TipoServicio);
        }

        public async Task<List<Registro>> GetByClienteIdAsync(int Id_Cliente)
        {
            return await _registroRepository.GetByClienteIdAsync(Id_Cliente);
        }
    }
}