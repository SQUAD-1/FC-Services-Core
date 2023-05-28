using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace backend_squad1.Services
{
    public class RemoveMidiaService
    {
        private readonly string _databaseConnectionString;

        public RemoveMidiaService(IConfiguration configuration)
        {
            _databaseConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task RemoveMedia(int idMidia)
        {
            using (MySqlConnection connection = new MySqlConnection(_databaseConnectionString))
            {
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Midia WHERE idMidia = @id";
                command.Parameters.AddWithValue("@id", idMidia);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
