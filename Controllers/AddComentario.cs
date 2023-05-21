using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MySql.Data.MySqlClient;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddComentarioController : ControllerBase
    {
        [HttpPost("adicionar-comentario/{idChamado}")]
        [Authorize]
        public IActionResult AdicionarComentario(int idChamado, Comentario comentario)
        {
            string connectionString = "server=gateway01.us-east-1.prod.aws.tidbcloud.com;port=4000;database=mydb;user=2yztCux73sSBMGV.root;password=A857G3OyIUoJOifl";
            using MySqlConnection connection = new MySqlConnection(connectionString);
            using MySqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO Comentario (IdChamado, Texto, DataComentario) VALUES (@IdChamado, @Texto, @DataComentario); SELECT LAST_INSERT_ID();";

            command.Parameters.AddWithValue("@IdChamado", idChamado);
            command.Parameters.AddWithValue("@Texto", comentario.Texto);
            command.Parameters.AddWithValue("@DataComentario", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

            connection.Open();
            int comentarioId = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return Ok(new { ComentarioId = comentarioId });
        }
    }
}