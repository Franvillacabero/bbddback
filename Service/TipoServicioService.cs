using Models;
using Back.Repository;

namespace Back.Services
{
    public class TipoServicioService : ITipoServicioService
    {
        private readonly ITipoServicioRepository _tipoServicioRepository;

        public TipoServicioService(ITipoServicioRepository tipoServicioRepository)
        {
            _tipoServicioRepository = tipoServicioRepository;
        }

        public async Task<List<TipoServicio>> GetAllAsync()
        {
            return await _tipoServicioRepository.GetAllAsync();
        }

        public async Task<TipoServicio?> GetByIdAsync(int id)
        {
            return await _tipoServicioRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(TipoServicio tipoServicio)
        {
            await _tipoServicioRepository.AddAsync(tipoServicio);
        }

        public async Task UpdateAsync(TipoServicio tipoServicio)
        {
            await _tipoServicioRepository.UpdateAsync(tipoServicio);
        }

        public async Task DeleteAsync(int id)
        {
            await _tipoServicioRepository.DeleteAsync(id);
        }
    }
}