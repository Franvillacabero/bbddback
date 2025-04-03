using Models;

namespace Back.Services
{
    public interface ITipoServicioService
    {
        Task<List<TipoServicio>> GetAllAsync();
        Task<TipoServicio?> GetByIdAsync(int id);
        Task AddAsync(TipoServicio tipoServicio);
        Task UpdateAsync(TipoServicio tipoServicio);
        Task DeleteAsync(int id);
    }
}