using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlterarSenhaController : ControllerBase
    {
        [HttpPut("{matricula}", Name = "Alterar Senha")]
        public IActionResult PutAlterarSenha(string matricula, [FromBody] AlterarSenhaRequest request)
        {
            string connectionString = "server=gateway01.us-east-1.prod.aws.tidbcloud.com;port=4000;database=mydb;user=2yztCux73sSBMGV.root;password=A857G3OyIUoJOifl";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT Senha FROM Empregado WHERE Matricula = @Matricula";
            command.Parameters.AddWithValue("@Matricula", matricula);
            connection.Open();
            string senhaAntiga = Convert.ToString(command.ExecuteScalar());
            connection.Close();

            if (senhaAntiga == null)
            {
                return NotFound("Usuário não encontrado");
            }

            string hashedSenhaAntiga;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(request.SenhaAntiga));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                hashedSenhaAntiga = builder.ToString();
            }

            if (hashedSenhaAntiga != senhaAntiga)
            {
                return BadRequest("Senha antiga incorreta");
            }

            string hashedNovaSenha;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(request.NovaSenha));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                hashedNovaSenha = builder.ToString();
            }

            command.CommandText = "UPDATE Empregado SET Senha = @Senha WHERE Matricula = @Matricula";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@Senha", hashedNovaSenha);
            command.Parameters.AddWithValue("@Matricula", matricula);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }
    }

    public class AlterarSenhaRequest
    {
        public string SenhaAntiga { get; set; }
        public string NovaSenha { get; set; }
    }
}
