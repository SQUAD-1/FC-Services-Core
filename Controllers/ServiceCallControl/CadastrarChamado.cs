using Microsoft.AspNetCore.Mvc;
using backend_squad1.Models;
using backend_squad1.Services;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("CadastroChamado")]
    public class ChamadoController : ControllerBase
    {
        private readonly CadastroChamadoService cadastroChamadoService;

        public ChamadoController(CadastroChamadoService cadastroChamadoService)
        {
            this.cadastroChamadoService = cadastroChamadoService;
        }

        [HttpPost]
        public IActionResult CadastrarChamado(CadastrarChamado chamado)
        {
            try
            {
                int id = cadastroChamadoService.CadastrarChamado(chamado);
                return Ok(id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
