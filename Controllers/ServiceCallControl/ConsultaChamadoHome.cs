using Microsoft.AspNetCore.Mvc;
using backend_squad1.Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using backend_squad1.DataModels;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultaChamadoController : ControllerBase
    {
        private readonly ConsultaChamadoService _consultaChamadoService;

        public ConsultaChamadoController(ConsultaChamadoService consultaChamadoService)
        {
            _consultaChamadoService = consultaChamadoService;
        }

        [HttpGet("{matricula}", Name = "GetChamadosByMatricula")]
        [Authorize]
        public IActionResult GetAllChamados(int matricula)
        {
            List<ConsultaChamado> chamados = _consultaChamadoService.GetChamadosByMatricula(matricula);
            return Ok(chamados);
        }
    }
}
