using MySql.Data.MySqlClient;
using Models;

namespace Back.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            var clientes = new List<Cliente>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Cliente";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var cliente = new Cliente
                            {
                                Id_Cliente = reader.GetInt32(0),
                                Nombre_Empresa = reader.GetString(1),
                                FechaRegistro = reader.GetDateTime(2),
                                Notas = reader.IsDBNull(3) ? null : reader.GetString(3),
                            };

                            clientes.Add(cliente);
                        }
                    }
                }
            }
            return clientes;
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            Cliente? cliente = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Cliente WHERE Id_Cliente = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            cliente = new Cliente
                            {
                                Id_Cliente = reader.GetInt32(0),
                                Nombre_Empresa = reader.GetString(1),
                                FechaRegistro = reader.GetDateTime(2),
                                Notas = reader.IsDBNull(3) ? null : reader.GetString(3),
                            };
                        }
                    }
                }
            }
            return cliente;
        }

        public async Task AddAsync(Cliente cliente)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Cliente (Nombre_Empresa, Notas) VALUES (@Nombre_Empresa, @Notas)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre_Empresa", cliente.Nombre_Empresa);
                    command.Parameters.AddWithValue("@Notas", cliente.Notas ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Cliente SET Nombre_Empresa = @Nombre_Empresa, Notas = @Notas WHERE Id_Cliente = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", cliente.Id_Cliente);
                    command.Parameters.AddWithValue("@Nombre_Empresa", cliente.Nombre_Empresa);
                    command.Parameters.AddWithValue("@Notas", cliente.Notas ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Cliente WHERE Id_Cliente = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}