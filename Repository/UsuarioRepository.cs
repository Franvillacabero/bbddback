using MySql.Data.MySqlClient;
using Models;

namespace Back.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            var usuarios = new List<Usuario>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Usuario";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var usuario = new Usuario
                            {
                                Id_Usuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Contraseña = reader.GetString(2),
                                Fecha_Registro = reader.GetDateTime(3),
                                EsAdmin = reader.GetBoolean(4), // Asumiendo que la columna 5 es EsAdmin
                                Clientes = new List<int>(5)
                            };

                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            Usuario? usuario = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Usuario WHERE Id_Usuario = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                Id_Usuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Contraseña = reader.GetString(2),
                                Fecha_Registro = reader.GetDateTime(3),
                                EsAdmin = reader.GetBoolean(4), // Asumiendo que la columna 5 es EsAdmin
                                Clientes = new List<int>(5)
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public async Task AddAsync(Usuario usuario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Usuario (Nombre, Contraseña, Fecha_Registro, esAdmin, Clientes) VALUES (@Nombre, @Contraseña, @Fecha_Registro, @EsAdmin, @Clientes)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                    command.Parameters.AddWithValue("@Fecha_Registro", usuario.Fecha_Registro);
                    command.Parameters.AddWithValue("@EsAdmin", usuario.EsAdmin);
                    command.Parameters.AddWithValue("@Clientes", usuario.Clientes); // Asumiendo que tienes una forma de manejar la lista de clientes

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Usuario SET Nombre = @Nombre, Contraseña = @Contraseña, Fecha_Registro = @Fecha_Registro, EsAmin = @EsAmin, Clientes = @Clientes WHERE Id_Usuario = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", usuario.Id_Usuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);                    
                    command.Parameters.AddWithValue("@Fecha_Registro", usuario.Fecha_Registro);
                    command.Parameters.AddWithValue("@EsAdmin", usuario.EsAdmin);
                    command.Parameters.AddWithValue("@Clientes", usuario.Clientes); // Asumiendo que tienes una forma de manejar la lista de clientes

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Usuario WHERE Id_Usuario = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Usuario?> GetByNameAndPasswordAsync(string name, string password)
        {
            Usuario? usuario = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Usuario WHERE Nombre = @Nombre AND Contraseña = @Contraseña";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", name);
                    command.Parameters.AddWithValue("@Contraseña", password);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                Id_Usuario = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Contraseña = reader.GetString(2),
                                Fecha_Registro = reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }

            if (usuario != null && usuario.Contraseña == password)
            {
                return usuario;
            }

            return null;
        }

        public async Task<bool> ActualizarContraseñaAsync(int idUsuario, string nuevaContraseña)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Usuario SET Contraseña = @NuevaContraseña WHERE Id_Usuario = @IdUsuario";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@NuevaContraseña", nuevaContraseña);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
    }
}
}   