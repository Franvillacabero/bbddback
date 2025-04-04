using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using Models;
using System.Text.Json;

namespace Back.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            var usuarios = new List<Usuario>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Usuario";
                using (var command = new MySqlCommand(query, connection))
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
                            EsAdmin = reader.GetBoolean(4),
                            Clientes = JsonSerializer.Deserialize<List<int>>(reader.GetString(5)) ?? new List<int>()
                        };

                        usuarios.Add(usuario);
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
                                EsAdmin = reader.GetBoolean(4),
                                Clientes = JsonSerializer.Deserialize<List<int>>(reader.GetString(5)) ?? new List<int>()
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

                string hashedPassword = _passwordHasher.HashPassword(usuario, usuario.Contraseña);

                string query = "INSERT INTO Usuario (Nombre, Contraseña, Fecha_Registro, EsAdmin, Clientes) VALUES (@Nombre, @Contraseña, @Fecha_Registro, @EsAdmin, @Clientes)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Contraseña", hashedPassword);
                    command.Parameters.AddWithValue("@Fecha_Registro", usuario.Fecha_Registro);
                    command.Parameters.AddWithValue("@EsAdmin", usuario.EsAdmin);

                    var clientesParam = new MySqlParameter("@Clientes", MySqlDbType.JSON);
                    clientesParam.Value = JsonSerializer.Serialize(usuario.Clientes);
                    command.Parameters.Add(clientesParam);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string hashedPassword = _passwordHasher.HashPassword(usuario, usuario.Contraseña);

                string query = "UPDATE Usuario SET Nombre = @Nombre, Contraseña = @Contraseña, Fecha_Registro = @Fecha_Registro, EsAdmin = @EsAdmin, Clientes = @Clientes WHERE Id_Usuario = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", usuario.Id_Usuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Contraseña", hashedPassword);  // Guardamos la contraseña hasheada
                    command.Parameters.AddWithValue("@Fecha_Registro", usuario.Fecha_Registro);
                    command.Parameters.AddWithValue("@EsAdmin", usuario.EsAdmin);

                    var clientesParam = new MySqlParameter("@Clientes", MySqlDbType.JSON);
                    clientesParam.Value = JsonSerializer.Serialize(usuario.Clientes);
                    command.Parameters.Add(clientesParam);

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

                string query = "SELECT * FROM Usuario WHERE Nombre = @Nombre";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", name);

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
                                EsAdmin = reader.GetBoolean(4),
                                Clientes = JsonSerializer.Deserialize<List<int>>(reader.GetString(5)) ?? new List<int>()
                            };
                        }
                    }
                }
            }

            if (usuario != null)
            {
                // Verificamos la contraseña hasheada usando PasswordHasher
                var verificationResult = _passwordHasher.VerifyHashedPassword(usuario, usuario.Contraseña, password);

                if (verificationResult == PasswordVerificationResult.Success)
                {
                    return usuario;  // Contraseña válida
                }
            }

            return null;  // Contraseña inválida
        }

        public async Task<bool> ActualizarContraseñaAsync(int idUsuario, string nuevaContraseña)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Hasheamos la nueva contraseña antes de actualizarla
                string hashedPassword = _passwordHasher.HashPassword(null, nuevaContraseña);

                string query = "UPDATE Usuario SET Contraseña = @NuevaContraseña WHERE Id_Usuario = @IdUsuario";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@NuevaContraseña", hashedPassword);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
