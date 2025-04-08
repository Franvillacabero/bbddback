using MySql.Data.MySqlClient;
using Models;

namespace Back.Repository
{
    public class RegistroRepository : IRegistroRepository
    {
        private readonly string _connectionString;

        public RegistroRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Registro>> GetAllAsync()
        {
            var registros = new List<Registro>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Registro";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var registro = new Registro
                            {
                                Id_Registro = reader.GetInt32(0),
                                Id_Cliente = reader.GetInt32(1),
                                Id_TipoServicio = reader.GetInt32(2),
                                Usuario = reader.GetString(3),
                                Contraseña = reader.GetString(4),
                                Notas = reader.GetString(5),
                                FechaCreacion = reader.GetDateTime(6),
                            };

                            registros.Add(registro);
                        }
                    }
                }
            }
            return registros;
        }

        public async Task<Registro?> GetByIdAsync(int id)
        {
            Registro? registro = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Registro WHERE Id_Registro = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            registro = new Registro
                            {
                                Id_Registro = reader.GetInt32(0),
                                Id_Cliente = reader.GetInt32(1),
                                Id_TipoServicio = reader.GetInt32(2),
                                Usuario = reader.GetString(3),
                                Contraseña = reader.GetString(4),
                                Notas = reader.GetString(5),
                                FechaCreacion = reader.GetDateTime(6),
                            };
                        }
                    }
                }
            }
            return registro;
        }

        public async Task AddAsync(Registro registro)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Encriptamos la contraseña antes de insertarla
                string encryptedPassword = AesEncryptionHelper.Encrypt(registro.Contraseña);

                string query = "INSERT INTO Registro (id_registro, id_cliente, id_tiposervicio, usuario, contraseña, notas) " +
                               "VALUES (@Id_Registro, @Id_Cliente, @Id_TipoServicio, @Usuario, @Contraseña, @Notas)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id_Registro", registro.Id_Registro);
                    command.Parameters.AddWithValue("@Id_Cliente", registro.Id_Cliente);
                    command.Parameters.AddWithValue("@Id_TipoServicio", registro.Id_TipoServicio);
                    command.Parameters.AddWithValue("@Usuario", registro.Usuario);
                    command.Parameters.AddWithValue("@Contraseña", encryptedPassword);  // Guardamos la contraseña encriptada
                    command.Parameters.AddWithValue("@Notas", registro.Notas ?? (object)DBNull.Value);  // Permitir valores nulos

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task UpdateAsync(Registro registro)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Encriptamos la contraseña antes de actualizarla
                string encryptedPassword = AesEncryptionHelper.Encrypt(registro.Contraseña);

                string query = "UPDATE Registro SET Id_Cliente = @Id_Cliente, Id_TipoServicio = @Id_TipoServicio, " +
                               "Usuario = @Usuario, Contraseña = @Contraseña, Notas = @Notas, Fecha_Registro = @Fecha_Registro " +
                               "WHERE Id_Registro = @Id_Registro";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id_Registro", registro.Id_Registro);
                    command.Parameters.AddWithValue("@Id_Cliente", registro.Id_Cliente);
                    command.Parameters.AddWithValue("@Id_TipoServicio", registro.Id_TipoServicio);
                    command.Parameters.AddWithValue("@Usuario", registro.Usuario);
                    command.Parameters.AddWithValue("@Contraseña", encryptedPassword);  // Guardamos la contraseña encriptada
                    command.Parameters.AddWithValue("@Notas", registro.Notas ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Fecha_Registro", registro.FechaCreacion);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Registro WHERE Id_Registro = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Registro>> GetByTipoServicioIdAsync(int Id_TipoServicio)
        {
            var registros = new List<Registro>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Registro WHERE Id_TipoServicio = @Id_TipoServicio";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id_TipoServicio", Id_TipoServicio);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var registro = new Registro
                            {
                                Id_Registro = reader.GetInt32(0),
                                Id_TipoServicio = reader.GetInt32(1),
                                Usuario = reader.GetString(2),
                                Contraseña = reader.GetString(3),
                                Notas = reader.GetString(4),
                                FechaCreacion = reader.GetDateTime(5),
                            };

                            registros.Add(registro);
                        }
                    }
                }
            }
            return registros;
        }

        public async Task<List<Registro>> GetByClienteIdAsync(int Id_Cliente)
        {
            var registros = new List<Registro>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Registro WHERE Id_Cliente = @Id_Cliente";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id_Cliente", Id_Cliente);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var registro = new Registro
                            {
                                Id_Registro = reader.GetInt32(0),
                                Id_Cliente = reader.GetInt32(1),
                                Id_TipoServicio = reader.GetInt32(2),
                                Usuario = reader.GetString(3),
                                Contraseña = reader.GetString(4),
                                Notas = reader.GetString(5),
                                FechaCreacion = reader.GetDateTime(6),
                            };

                            registros.Add(registro);
                        }
                    }
                }
            }
            return registros;
        }
    }
}