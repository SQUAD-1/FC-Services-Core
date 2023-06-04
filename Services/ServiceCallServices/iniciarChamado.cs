using System;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace backend_squad1.Services
{
    public class ChamadoService
    {
        private readonly string _databaseConnectionString;

        public ChamadoService(IConfiguration configuration)
        {
            _databaseConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool MarcarChamadoEmAndamento(int id)
        {
            MySqlConnection connection = new MySqlConnection(_databaseConnectionString);
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT Status FROM Chamado WHERE idChamado = @Id";
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            object result = command.ExecuteScalar();
            connection.Close();

            if (result != null && string.Equals(result.ToString(), "Em andamento", StringComparison.OrdinalIgnoreCase))
            {
                // Chamado já está em andamento, retorna falso
                return false;
            }

            bool success = false;
            try
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                // Atualiza o status do chamado para em andamento
                command.CommandText = "UPDATE Chamado SET Status = 'Em andamento' WHERE idChamado = @Id";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();

                // Insere o registro de atividade
                command.CommandText = "INSERT INTO RegistroAtividade (horarioUltimo, informaoUltima, Chamado_idChamado) VALUES (@HorarioUltimo, @InformacaoUltima, @ChamadoId)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@HorarioUltimo", DateTime.Now.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("@InformacaoUltima", "Alteração do status para Em andamento");
                command.Parameters.AddWithValue("@ChamadoId", id);
                command.ExecuteNonQuery();

                transaction.Commit();
                success = true;
            }
            catch (Exception ex)
            {
                // Trate o erro conforme sua necessidade
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return success;
        }

        public bool MarcarChamadoFinalizado(int id)
        {
            MySqlConnection connection = new MySqlConnection(_databaseConnectionString);
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT Status FROM Chamado WHERE idChamado = @Id";
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            object result = command.ExecuteScalar();
            connection.Close();

            if (result != null && string.Equals(result.ToString(), "Finalizado", StringComparison.OrdinalIgnoreCase))
            {
                // Chamado já está finalizado, retorna falso
                return false;
            }

            bool success = false;
            try
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                // Atualiza o status do chamado para finalizado
                command.CommandText = "UPDATE Chamado SET Status = 'Finalizado' WHERE idChamado = @Id";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();

                // Insere o registro de atividade
                command.CommandText = "INSERT INTO RegistroAtividade (horarioUltimo, informaoUltima, Chamado_idChamado) VALUES (@HorarioUltimo, @InformacaoUltima, @ChamadoId)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@HorarioUltimo", DateTime.Now.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("@InformacaoUltima", "Alteração do status para Finalizado");
                command.Parameters.AddWithValue("@ChamadoId", id);
                command.ExecuteNonQuery();

                transaction.Commit();
                success = true;
            }
            catch (Exception ex)
            {
                // Trate o erro conforme sua necessidade
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return success;
        }
    }
}
