using Models;

namespace Back.Repository
{
    public interface ICampoRepository
    {
        Task<List<Campo>> GetAllAsync();
        Task<Campo?> GetByIdAsync(int id);
        Task AddAsync(Campo campo);
        Task UpdateAsync(Campo campo);
        Task DeleteAsync(int id);
    }
}