using Models;

namespace Back.Repository
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task<Usuario?> GetByNameAndPasswordAsync(string name, string password);
        Task<bool> ActualizarContraseñaAsync(int idUsuario, string nuevaContraseña);
        Task<Usuario?> GetByNameAsync(string nombre);
        Task<List<Usuario>> GetAllNoAdminAsync();

    }
}