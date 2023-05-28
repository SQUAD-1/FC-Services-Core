using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using backend_squad1.DataModels;


namespace backend_squad1.Services
{
    public class ConsultaChamadoService
    {
        private readonly string _databaseConnectionString;

        public ConsultaChamadoService(IConfiguration configuration)
        {
            _databaseConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ConsultaChamado> GetChamadosByMatricula(int matricula)
        {
            using MySqlConnection connection = new MySqlConnection(_databaseConnectionString);
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Chamado WHERE Empregado_Matricula = @Matricula";
            command.Parameters.AddWithValue("@Matricula", matricula);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            List<ConsultaChamado> chamados = new List<ConsultaChamado>();

            while (reader.Read())
            {
                int idChamado = reader.GetInt32("idChamado");
                string nome = reader.GetString("Nome");
                DateTime dataRelato = reader.GetDateTime("DataRelato");
                string descricao = reader.GetString("Descricao");
                string prioridade = reader.GetString("Prioridade");
                string horarioAbertura = reader.GetString("HorarioAbertura");
                string horarioUltimaAtualizacao = reader.GetString("horarioUltimaAtualizacao");
                string status = reader.GetString("Status");
                string tempoDecorrido = reader.GetString("TempoDecorrido");
                int empregado_Matricula = reader.GetInt32("Empregado_Matricula");
                string tipo = reader.GetString("Tipo");

                ConsultaChamado chamado = new ConsultaChamado
                {
                    idChamado = idChamado,
                    Nome = nome,
                    DataRelato = dataRelato.ToString("dd/MM/yyyy"),
                    Descricao = descricao,
                    Prioridade = prioridade,
                    HorarioAbertura = horarioAbertura,
                    HorarioUltimaAtualizacao = horarioUltimaAtualizacao,
                    Status = status,
                    TempoDecorrido = tempoDecorrido,
                    Empregado_Matricula = empregado_Matricula,
                    Tipo = tipo,
                };

                chamados.Add(chamado);
            }

            return chamados;
        }
    }
}
