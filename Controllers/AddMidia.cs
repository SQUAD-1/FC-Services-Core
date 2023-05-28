using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend_squad1.Services;

namespace backend_squad1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddMidiaController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly AddMidiaService _addMidiaService;

        public AddMidiaController(IWebHostEnvironment env, AddMidiaService addMidiaService)
        {
            _env = env;
            _addMidiaService = addMidiaService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files, int chamadoIdChamado)
        {
            try
            {
                if (chamadoIdChamado <= 0)
                {
                    return BadRequest("O ID do chamado deve ser maior que zero.");
                }

                string googleCredentialsPath = "fc-services-ba67f-firebase-adminsdk-nytje-1959376e26.json";
                string bucketName = "fc-services-ba67f.appspot.com";
                string databaseConnectionString = "server=gateway01.us-east-1.prod.aws.tidbcloud.com;port=4000;database=mydb;user=2yztCux73sSBMGV.root;password=A857G3OyIUoJOifl";

                var urls = await _addMidiaService.UploadMedia(files, chamadoIdChamado, googleCredentialsPath, bucketName, databaseConnectionString);

                return Ok(new { urls });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer upload do arquivo: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }
    }
}
