using Microsoft.AspNetCore.Mvc;
using backend_squad1.Services;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("/acaoChamado")]
    public class AcaoChamadoController : ControllerBase
    {
        private readonly ChamadoService chamadoService;

        public AcaoChamadoController(ChamadoService chamadoService)
        {
            this.chamadoService = chamadoService;
        }

        [HttpPost("iniciarChamado/{id}")]
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

        [HttpPost("finalizarChamado/{id}")]
        public IActionResult MarcarChamadoFinalizado(int id)
        {
            try
            {
                bool success = chamadoService.MarcarChamadoFinalizado(id);
                if (success)
                {
                    return Ok("Chamado marcado como Finalizado.");
                }
                else
                {
                    return BadRequest("O chamado já está Finalizado.");
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
