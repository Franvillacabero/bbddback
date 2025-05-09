using Back.Repository;
using Models;

namespace Back.Services
{
    public class CampoService : ICampoService
    {
        private readonly ICampoRepository _campoRepository;

        public CampoService(ICampoRepository campoRepository)
        {
            _campoRepository = campoRepository;
        }

        public async Task<List<Campo>> GetAllAsync()
        {
            return await _campoRepository.GetAllAsync();
        }

        public async Task<Campo?> GetByIdAsync(int id)
        {
            return await _campoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Campo campo)
        {
            await _campoRepository.AddAsync(campo);
        }

        public async Task UpdateAsync(Campo campo)
        {
            await _campoRepository.UpdateAsync(campo);
        }

        public async Task DeleteAsync(int id)
        {
            await _campoRepository.DeleteAsync(id);
        }
    }
}