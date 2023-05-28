using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using backend_squad1.DataModels;

namespace backend_squad1.Services
{
    public class ConsultaChamadoIdService
    {
        private readonly string _databaseConnectionString;

        public ConsultaChamadoIdService(IConfiguration configuration)
        {
            _databaseConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ConsultaChamado> GetChamadosById(int idChamado)
        {
            MySqlConnection connection = new MySqlConnection(_databaseConnectionString);
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT c.*, m.idMidia, m.linkMidia, m.tipoMidia FROM Chamado c LEFT JOIN Midia m ON c.idChamado = m.Chamado_idChamado WHERE c.idChamado = @idChamado";
            command.Parameters.AddWithValue("@idChamado", idChamado);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            List<ConsultaChamado> chamados = new List<ConsultaChamado>();

            while (reader.Read())
            {
                int chamadoId = reader.GetInt32("idChamado");
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
                int idMidia = reader.IsDBNull(reader.GetOrdinal("idMidia")) ? 0 : reader.GetInt32("idMidia");
                string linkMidia = reader.IsDBNull(reader.GetOrdinal("linkMidia")) ? null : reader.GetString("linkMidia");
                string tipoMidia = reader.IsDBNull(reader.GetOrdinal("tipoMidia")) ? null : reader.GetString("tipoMidia");

                ConsultaChamado chamado = chamados.Find(c => c.idChamado == chamadoId);

                if (chamado == null)
                {
                    chamado = new ConsultaChamado
                    {
                        idChamado = chamadoId,
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
                        LinkMidia = new List<LinkMidia>()
                    };

                    chamados.Add(chamado);
                }

                if (!string.IsNullOrEmpty(linkMidia))
                {
                    chamado.LinkMidia.Add(new LinkMidia { IdMidia = idMidia, Link = linkMidia, TipoMidia = tipoMidia });
                }
            }

            return chamados;
        }
    }
}
