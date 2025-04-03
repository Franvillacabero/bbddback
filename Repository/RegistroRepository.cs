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
                                Id_TipoServicio = reader.GetInt32(1),
                                Usuario = reader.GetString(2),
                                Contraseña = reader.GetString(3),
                                Notas = reader.GetString(4),
                                FechaCreacion = reader.GetDateTime(5),
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

                string query = "INSERT INTO Registro (Id_Usuario, Id_TipoServicio, Fecha_Registro, Estado) VALUES (@Id_Usuario, @Id_TipoServicio, @Fecha_Registro, @Estado)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id_TipoServicio", registro.Id_TipoServicio);
                    /*command.Parameters.AddWithValue("@Fecha_Registro", registro.Fecha_Registro);
                    command.Parameters.AddWithValue("@Estado", registro.Estado);*/

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Registro registro)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Registro SET Id_Usuario = @Id_Usuario, Id_TipoServicio = @Id_TipoServicio, Fecha_Registro = @Fecha_Registro, Estado = @Estado WHERE Id_Registro = @Id_Registro";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id_TipoServicio", registro.Id_TipoServicio);
                    /*command.Parameters.AddWithValue("@Fecha_Registro", registro.Fecha_Registro);
                    command.Parameters.AddWithValue("@Estado", registro.Estado);*/
                    command.Parameters.AddWithValue("@Id_Registro", registro.Id_Registro);

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
    }
}