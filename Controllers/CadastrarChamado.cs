using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroChamadoController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public IActionResult CadastroChamado(CadastrarChamado chamado)
        {
            string connectionString = "server=gateway01.us-east-1.prod.aws.tidbcloud.com;port=4000;database=mydb;user=2yztCux73sSBMGV.root;password=A857G3OyIUoJOifl";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = connection.CreateCommand();

            // Consulta na tabela Empregado para obter o Filial_idFilial com base na matrícula
            command.CommandText = "SELECT Filial_idFilial FROM Empregado WHERE Matricula = @Matricula";
            command.Parameters.AddWithValue("@Matricula", chamado.Empregado_Matricula);

            int filialId;

            connection.Open();
            object result = command.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out filialId))
            {
              
                string prioridade;

                if (chamado.Tipo == "Falta de material")
                {
                    if (filialId == 1)
                    {
                        prioridade = "Prioridade 1";
                    }
                    else if (filialId == 2)
                    {
                        prioridade = "Prioridade 2";
                    }
                    else if (filialId == 3)
                    {
                        prioridade = "Prioridade 3";
                    }
                    else if (filialId == 4)
                    {
                        prioridade = "Prioridade 4";
                    }
                    else if (filialId == 5)
                    {
                        prioridade = "Prioridade 5";
                    }
                    else if (filialId == 6)
                    {
                        prioridade = "Prioridade 6";
                    }
                    else if (filialId == 7)
                    {
                        prioridade = "Prioridade 7";
                    }
                    else if (filialId == 8)
                    {
                        prioridade = "Prioridade 8";
                    }
                    else
                    {
                        prioridade = "Prioridade Padrão";
                    }
                }
                else if (chamado.Tipo == "Problema com a internet")
                {
                    if (filialId == 1)
                    {
                        prioridade = "Internet Prioridade 1";
                    }
                    else if (filialId == 2)
                    {
                        prioridade = "Internet Prioridade 2";
                    }
                    else if (filialId == 3)
                    {
                        prioridade = "Internet Prioridade 3";
                    }
                    else if (filialId == 4)
                    {
                        prioridade = "Internet Prioridade 4";
                    }
                    else if (filialId == 5)
                    {
                        prioridade = "Internet Prioridade 5";
                    }
                    else if (filialId == 6)
                    {
                        prioridade = "Internet Prioridade 6";
                    }
                    else if (filialId == 7)
                    {
                        prioridade = "Internet Prioridade 7";
                    }
                    else if (filialId == 8)
                    {
                        prioridade = "Internet Prioridade 8";
                    }
                    else
                    {
                        prioridade = "Internet Prioridade Padrão";
                    }
                }
                else if (chamado.Tipo == "Solicitação de limpeza")
                {
                    if (filialId == 1)
                    {
                        prioridade = "Limpeza Prioridade 1";
                    }
                    else if (filialId == 2)
                    {
                        prioridade = "Limpeza Prioridade 2";
                    }
                    else if (filialId == 3)
                    {
                        prioridade = "Limpeza Prioridade 3";
                    }
                    else if (filialId == 4)
                    {
                        prioridade = "Limpeza Prioridade 4";
                    }
                    else if (filialId == 5)
                    {
                        prioridade = "Limpeza Prioridade 5";
                    }
                    else if (filialId == 6)
                    {
                        prioridade = "Limpeza Prioridade 6";
                    }
                    else if (filialId == 7)
                    {
                        prioridade = "Limpeza Prioridade 7";
                    }
                    else if (filialId == 8)
                    {
                        prioridade = "Limpeza Prioridade 8";
                    }
                    else
                    {
                        prioridade = "Limpeza Prioridade Padrão";
                    }
                }
                else if (chamado.Tipo == "Solicitação de recurso")
                {
                    if (filialId == 1)
                    {
                        prioridade = "Recurso Prioridade 1";
                    }
                    else if (filialId == 2)
                    {
                        prioridade = "Recurso Prioridade 2";
                    }
                    else if (filialId == 3)
                    {
                        prioridade = "Recurso Prioridade 3";
                    }
                    else if (filialId == 4)
                    {
                        prioridade = "Recurso Prioridade 4";
                    }
                    else if (filialId == 5)
                    {
                        prioridade = "Recurso Prioridade 5";
                    }
                    else if (filialId == 6)
                    {
                        prioridade = "Recurso Prioridade 6";
                    }
                    else if (filialId == 7)
                    {
                        prioridade = "Recurso Prioridade 7";
                    }
                    else if (filialId == 8)
                    {
                        prioridade = "Recurso Prioridade 8";
                    }
                    else
                    {
                        prioridade = "Recurso Prioridade Padrão";
                    }
                }
                else if (chamado.Tipo == "Objeto perdido")
                {
                    if (filialId == 1)
                    {
                        prioridade = "Objeto Perdido Prioridade 1";
                    }
                    else if (filialId == 2)
                    {
                        prioridade = "Objeto Perdido Prioridade 2";
                    }
                    else if (filialId == 3)
                    {
                        prioridade = "Objeto Perdido Prioridade 3";
                    }
                    else if (filialId == 4)
                    {
                        prioridade = "Objeto Perdido Prioridade 4";
                    }
                    else if (filialId == 5)
                    {
                        prioridade = "Objeto Perdido Prioridade 5";
                    }
                    else if (filialId == 6)
                    {
                        prioridade = "Objeto Perdido Prioridade 6";
                    }
                    else if (filialId == 7)
                    {
                        prioridade = "Objeto Perdido Prioridade 7";
                    }
                    else if (filialId == 8)
                    {
                        prioridade = "Objeto Perdido Prioridade 8";
                    }
                    else
                    {
                        prioridade = "Objeto Perdido Prioridade Padrão";
                    }
                }
                else
                {
                    if (filialId == 1)
                    {
                        prioridade = "Prioridade 1 Padrão";
                    }
                    else if (filialId == 2)
                    {
                        prioridade = "Prioridade 2 Padrão";
                    }
                    else if (filialId == 3)
                    {
                        prioridade = "Prioridade 3 Padrão";
                    }
                    else if (filialId == 4)
                    {
                        prioridade = "Prioridade 4 Padrão";
                    }
                    else if (filialId == 5)
                    {
                        prioridade = "Prioridade 5 Padrão";
                    }
                    else if (filialId == 6)
                    {
                        prioridade = "Prioridade 6 Padrão";
                    }
                    else if (filialId == 7)
                    {
                        prioridade = "Prioridade 7 Padrão";
                    }
                    else if (filialId == 8)
                    {
                        prioridade = "Prioridade 8 Padrão";
                    }
                    else
                    {
                        prioridade = "Prioridade Padrão";
                    }
                }

                // Continuação do código para a inserção do chamado com a prioridade definida

                command.CommandText = "INSERT INTO Chamado (Nome, DataRelato, Descricao, Prioridade, HorarioAbertura, horarioUltimaAtualizacao, Status, TempoDecorrido, Empregado_Matricula, Tipo) VALUES (@Nome, @DataRelato, @Descricao, @Prioridade, @HorarioAbertura, @HorarioUltimaAtualizacao, @Status, @TempoDecorrido, @Empregado_Matricula, @Tipo); SELECT LAST_INSERT_ID();";

                command.Parameters.AddWithValue("@Nome", chamado.Nome);
                command.Parameters.AddWithValue("@Descricao", chamado.Descricao);
                command.Parameters.AddWithValue("@HorarioAbertura", DateTime.Now.ToString("HH:mm:ss"));
                command.Parameters.AddWithValue("@HorarioUltimaAtualizacao", "00:00:00");
                command.Parameters.AddWithValue("@Status", "Aberto");
                command.Parameters.AddWithValue("@TempoDecorrido", "00:00:00");
                command.Parameters.AddWithValue("@Empregado_Matricula", chamado.Empregado_Matricula);
                command.Parameters.AddWithValue("@Tipo", chamado.Tipo);
                command.Parameters.AddWithValue("@Prioridade", prioridade);

                if (!DateTime.TryParseExact(chamado.DataRelato, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataRelato))
                {
                    connection.Close();
                    return BadRequest("A data de relato está em um formato inválido.");
                }
                chamado.DataRelato = dataRelato.ToString("yyyy/MM/dd");
                command.Parameters.AddWithValue("@DataRelato", chamado.DataRelato);

                int id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();

                return Ok(new { Id = id });
            }
            else
            {
                connection.Close();
                return BadRequest("Não foi possível encontrar a matrícula do empregado ou o valor de Filial_idFilial.");
            }
        }
    }
}
