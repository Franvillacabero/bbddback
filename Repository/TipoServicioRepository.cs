using MySql.Data.MySqlClient;
using Models;

namespace Back.Repository
{
    public class TipoServicioRepository : ITipoServicioRepository
    {
        private readonly string _connectionString;

        public TipoServicioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<TipoServicio>> GetAllAsync()
        {
            var tipoServicios = new List<TipoServicio>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM TipoServicio";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var tipoServicio = new TipoServicio
                            {
                                Id_TipoServicio = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                            };

                            tipoServicios.Add(tipoServicio);
                        }
                    }
                }
            }
            return tipoServicios;
        }

        public async Task<TipoServicio?> GetByIdAsync(int id)
        {
            TipoServicio? tipoServicio = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM TipoServicio WHERE Id_TipoServicio = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            tipoServicio = new TipoServicio
                            {
                                Id_TipoServicio = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                            };
                        }
                    }
                }
            }
            return tipoServicio;
        }

        public async Task AddAsync(TipoServicio tipoServicio)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO TipoServicio (Nombre) VALUES (@Nombre)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", tipoServicio.Nombre);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(TipoServicio tipoServicio)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE TipoServicio SET Nombre = @Nombre WHERE Id_TipoServicio = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", tipoServicio.Id_TipoServicio);
                    command.Parameters.AddWithValue("@Nombre", tipoServicio.Nombre);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM TipoServicio WHERE Id_TipoServicio = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}