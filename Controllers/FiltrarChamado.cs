using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FiltroChamadoController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetChamadosPorFiltro(int matricula, string? nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                nome = null;
            }
            
            string connectionString = "server=gateway01.us-east-1.prod.aws.tidbcloud.com;port=4000;database=mydb;user=2yztCux73sSBMGV.root;password=A857G3OyIUoJOifl";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = connection.CreateCommand();

            string query = "SELECT c.*, m.idMidia, m.linkMidia, m.tipoMidia FROM Chamado c LEFT JOIN Midia m ON c.idChamado = m.Chamado_idChamado WHERE c.Empregado_Matricula = @matricula";
            if (!string.IsNullOrEmpty(nome))
            {
                query += " AND c.Nome LIKE @nome";
                command.Parameters.AddWithValue("@nome", "%" + nome + "%");
            }
            command.Parameters.AddWithValue("@matricula", matricula);
            command.CommandText = query;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            List<ConsultaChamado> chamados = new List<ConsultaChamado>();

            while (reader.Read())
            {
                int chamadoId = reader.GetInt32("idChamado");
                string chamadoNome = reader.GetString("Nome");
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
                        Nome = chamadoNome,
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

            return Ok(chamados);
        }
    }
}
