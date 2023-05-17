using System;
using System.IO;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using backend_squad1;

namespace backend_squad1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddMidiaController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public AddMidiaController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string tipoMidia, int chamadoIdChamado)
        {
            try
            {
                if (string.IsNullOrEmpty(tipoMidia))
                {
                    return BadRequest("O tipo de mídia não pode ser vazio.");
                }

                if (chamadoIdChamado <= 0)
                {
                    return BadRequest("O ID do chamado deve ser maior que zero.");
                }

                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "fc-services-ba67f-firebase-adminsdk-nytje-1959376e26.json");

                var storage = StorageClient.Create();
                var filename = $"{DateTime.Now:yyyyMMddHHmmss}-{file.FileName}";
                var bucketName = "fc-services-ba67f.appspot.com";
                var objectName = filename;

                using (var stream = file.OpenReadStream())
                {
                    var contentType = file.ContentType;
                    var result = await storage.UploadObjectAsync(bucketName, objectName, contentType, stream);
                }

                var url = $"https://firebasestorage.googleapis.com/v0/b/{bucketName}/o/{Uri.EscapeDataString(filename)}?alt=media";

                var midia = new Midia
                {
                    TipoMidia = tipoMidia,
                    LinkMidia = url,
                    ChamadoIdChamado = chamadoIdChamado
                };

                string connectionString = "server=gateway01.us-east-1.prod.aws.tidbcloud.com;port=4000;database=mydb;user=2yztCux73sSBMGV.root;password=A857G3OyIUoJOifl";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO Midia (tipoMidia, linkMidia, chamado_idChamado) VALUES (@tipoMidia, @linkMidia, @chamadoIdChamado)";
                    command.Parameters.AddWithValue("@tipoMidia", midia.TipoMidia);
                    command.Parameters.AddWithValue("@linkMidia", midia.LinkMidia);
                    command.Parameters.AddWithValue("@chamadoIdChamado", midia.ChamadoIdChamado);

                    command.ExecuteNonQuery();
                }

                return Ok(new { url });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer upload do arquivo: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }
    }
}
