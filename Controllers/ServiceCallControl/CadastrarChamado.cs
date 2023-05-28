using Microsoft.AspNetCore.Mvc;
using backend_squad1.Models;
using backend_squad1.Services;
using backend_squad1.DataModels;


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
        public IActionResult CadastrarChamado(ServiceCall chamado)
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
