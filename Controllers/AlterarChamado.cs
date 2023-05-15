using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlterarChamadoController : ControllerBase
    {
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult AlterarChamado(int id, Chamado chamado)
        {
            string connectionString = "server=containers-us-west-209.railway.app;port=6938;database=railway;user=root;password=5cu1Y8DVEYLMeej8yleH";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "UPDATE Chamado SET Nome = @Nome, DataRelato = @DataRelato, Descricao = @Descricao, Prioridade = @Prioridade, HorarioAbertura = @HorarioAbertura, HorarioUltimaAtualizacao = @HorarioUltimaAtualizacao, Status = @Status, TempoDecorrido = @TempoDecorrido, Empregado_Matricula = @Empregado_Matricula, Tipo = @Tipo WHERE Id = @Id";

            command.Parameters.AddWithValue("@Nome", chamado.Nome);
            command.Parameters.AddWithValue("@DataRelato", chamado.DataRelato);
            command.Parameters.AddWithValue("@Descricao", chamado.Descricao);
            command.Parameters.AddWithValue("@Prioridade", chamado.Prioridade);
            command.Parameters.AddWithValue("@HorarioAbertura", chamado.HorarioAbertura);
            command.Parameters.AddWithValue("@HorarioUltimaAtualizacao", chamado.HorarioUltimaAtualizacao);
            command.Parameters.AddWithValue("@Status", chamado.Status);
            command.Parameters.AddWithValue("@TempoDecorrido", chamado.TempoDecorrido);
            command.Parameters.AddWithValue("@Empregado_Matricula", chamado.Empregado_Matricula);
            command.Parameters.AddWithValue("@Tipo", chamado.Tipo);
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            if (!DateTime.TryParseExact(chamado.DataRelato, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataRelato))
            {
                return BadRequest("A data de relato está em um formato inválido.");
            }

            if (!TimeSpan.TryParse(chamado.HorarioAbertura, out TimeSpan horarioAbertura))
            {
                return BadRequest("O valor passado para o campo 'HorarioAbertura' não é uma hora válida.");
            }

            if (!TimeSpan.TryParse(chamado.TempoDecorrido, out TimeSpan tempoDecorrido))
            {
                return BadRequest("O valor passado para o campo 'TempoDecorrido' não é uma hora válida.");
            }

            if (!DateTime.TryParse(chamado.HorarioUltimaAtualizacao, out DateTime horarioUltimaAtualizacao))
            {
                return BadRequest("O valor passado para o campo 'HorarioUltimaAtualizacao' não é uma data válida.");
            }

            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();

            if (rowsAffected > 0)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
