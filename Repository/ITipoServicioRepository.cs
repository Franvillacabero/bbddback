using Models;

namespace Back.Repository
{
    public interface ITipoServicioRepository
    {
        Task<List<TipoServicio>> GetAllAsync();
        Task<TipoServicio?> GetByIdAsync(int id);
        Task AddAsync(TipoServicio tipoServicio);
        Task UpdateAsync(TipoServicio tipoServicio);
        Task DeleteAsync(int id);
    }
}