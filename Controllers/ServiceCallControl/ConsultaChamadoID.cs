using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend_squad1.Services;
using backend_squad1.DataModels;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultaChamadoIdController : ControllerBase
    {
        private readonly ConsultaChamadoIdService _consultaChamadoIdService;

        public ConsultaChamadoIdController(ConsultaChamadoIdService consultaChamadoIdService)
        {
            _consultaChamadoIdService = consultaChamadoIdService;
        }

        [HttpGet("{idChamado}", Name = "GetChamadosByidChamado")]
        [Authorize]
        public IActionResult GetAllChamadosId(int idChamado)
        {
            var chamados = _consultaChamadoIdService.GetChamadosById(idChamado);
            return Ok(chamados);
        }
    }
}
