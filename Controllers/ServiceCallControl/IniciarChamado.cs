using Microsoft.AspNetCore.Mvc;
using backend_squad1.Services;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IniciarChamadoController : ControllerBase
    {
        private readonly ChamadoService chamadoService;

        public IniciarChamadoController(ChamadoService chamadoService)
        {
            this.chamadoService = chamadoService;
        }

        [HttpPost("{id}")]
        public IActionResult MarcarChamadoEmAndamento(int id)
        {
            try
            {
                bool success = chamadoService.MarcarChamadoEmAndamento(id);
                if (success)
                {
                    return Ok("Chamado marcado como Em andamento.");
                }
                else
                {
                    return BadRequest("O chamado já está Em andamento.");
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
