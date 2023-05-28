using System;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;


namespace backend_squad1.Services
{
    public class CadastroChamadoService
    {
        private readonly string _databaseConnectionString;

        public CadastroChamadoService(IConfiguration configuration)
        {
            _databaseConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int CadastrarChamado(CadastrarChamado chamado)
        {
            MySqlConnection connection = new MySqlConnection(_databaseConnectionString);
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO Chamado (Nome, DataRelato, Descricao, Prioridade, HorarioAbertura, horarioUltimaAtualizacao, Status, TempoDecorrido, Empregado_Matricula, Tipo) VALUES (@Nome, @DataRelato, @Descricao, @Prioridade, @HorarioAbertura, @HorarioUltimaAtualizacao, @Status, @TempoDecorrido, @Empregado_Matricula, @Tipo); SELECT LAST_INSERT_ID();";

            command.Parameters.AddWithValue("@Nome", chamado.Nome);
            command.Parameters.AddWithValue("@Descricao", chamado.Descricao);
            command.Parameters.AddWithValue("@HorarioAbertura", DateTime.Now.ToString("HH:mm:ss"));
            command.Parameters.AddWithValue("@HorarioUltimaAtualizacao", "00:00:00");
            command.Parameters.AddWithValue("@Status", "Aberto");
            command.Parameters.AddWithValue("@TempoDecorrido", "00:00:00");
            command.Parameters.AddWithValue("@Empregado_Matricula", chamado.Empregado_Matricula);
            command.Parameters.AddWithValue("@Tipo", chamado.Tipo);
            command.Parameters.AddWithValue("@Prioridade", ObterPrioridadePorTipo(chamado.Tipo));

            if (!DateTime.TryParseExact(chamado.DataRelato, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dataRelato))
            {
                throw new ArgumentException("A data de relato está em um formato inválido.");
            }
            chamado.DataRelato = dataRelato.ToString("yyyy/MM/dd");
            command.Parameters.AddWithValue("@DataRelato", chamado.DataRelato);

            connection.Open();
            int id = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return id;
        }

        private string ObterPrioridadePorTipo(string tipo)
        {
            if (tipo == "Falta de material")
            {
                return "Prioridade";
            }
            else if (tipo == "Problema com a internet")
            {
                return "Internet Prioridade";
            }
            else if (tipo == "Solicitação de limpeza")
            {
                return "Limpeza Prioridade";
            }
            else if (tipo == "Solicitação de recurso")
            {
                return "Recurso Prioridade";
            }
            else if (tipo == "Objeto perdido")
            {
                return "Objeto Perdido Prioridade";
            }
            else
            {
                return "Prioridade";
            }
        }
    }
}
