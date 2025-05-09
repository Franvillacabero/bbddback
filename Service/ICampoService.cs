using Models;

namespace Back.Services
{
    public interface ICampoService
    {
        Task<List<Campo>> GetAllAsync();
        Task<Campo?> GetByIdAsync(int id);
        Task AddAsync(Campo campo);
        Task UpdateAsync(Campo campo);
        Task DeleteAsync(int id);
    }
}