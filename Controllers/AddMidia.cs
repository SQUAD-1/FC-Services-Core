using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using backend_squad1.Services;

namespace backend_squad1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddMidiaController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly AddMidiaService _addMidiaService;
        private readonly IConfiguration _configuration;

        public AddMidiaController(IWebHostEnvironment env, AddMidiaService addMidiaService, IConfiguration configuration)
        {
            _env = env;
            _addMidiaService = addMidiaService;
            _configuration = configuration;
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
                string databaseConnectionString = _configuration.GetConnectionString("DefaultConnection");

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
