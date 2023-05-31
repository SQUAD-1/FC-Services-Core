using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend_squad1.Services;
using backend_squad1.DataModels;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FiltroChamadoController : ControllerBase
    {
        private readonly FiltrarChamadosService _filtrarChamadosService;

        public FiltroChamadoController(FiltrarChamadosService filtrarChamadosService)
        {
            _filtrarChamadosService = filtrarChamadosService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetChamadosPorFiltro(int matricula, string nome)
        {
            var chamados = _filtrarChamadosService.GetChamadosPorFiltro(matricula, nome);
            return Ok(chamados);
        }
    }
}