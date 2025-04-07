using Models;

namespace Back.Repository
{
    public interface IRegistroRepository
    {
        Task<List<Registro>> GetAllAsync();
        Task<Registro?> GetByIdAsync(int id);
        Task AddAsync(Registro registro);
        Task UpdateAsync(Registro registro);
        Task DeleteAsync(int id);
        Task<List<Registro>> GetByTipoServicioIdAsync(int Id_TipoServicio);

        Task<List<Registro>> GetByClienteIdAsync(int Id_Cliente);
    }
}