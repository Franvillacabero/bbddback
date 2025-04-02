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
                                Correo = reader.GetString(3),
                                Fecha_Registro = reader.GetDateTime(4)
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
                                Correo = reader.GetString(3),
                                Fecha_Registro = reader.GetDateTime(4)
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

                string query = "INSERT INTO Usuario (Nombre, Apellido, Contraseña, Correo, Fecha_Registro) VALUES (@Nombre, @Apellido, @Contraseña, @Correo, @Fecha_Registro)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                    command.Parameters.AddWithValue("@Correo", usuario.Correo);
                    command.Parameters.AddWithValue("@Fecha_Registro", usuario.Fecha_Registro);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, Contraseña = @Contraseña, Correo = @Correo WHERE Id_Usuario = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", usuario.Id_Usuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                    command.Parameters.AddWithValue("@Correo", usuario.Correo);

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

        public async Task<Usuario?> GetByEmailAndPasswordAsync(string email, string password)
        {
            Usuario? usuario = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Usuario WHERE Correo = @Correo AND Contraseña= @Contraseña";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Correo", email);
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
                                Correo = reader.GetString(3),
                                Fecha_Registro = reader.GetDateTime(4)
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