using Microsoft.AspNetCore.Mvc;
using backend_squad1.Services;
using backend_squad1.DataModels;


namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastrarUsuarioController : ControllerBase
    {
        private readonly ICadastrarUsuarioService _cadastrarUsuarioService;

        public CadastrarUsuarioController(ICadastrarUsuarioService cadastrarUsuarioService)
        {
            _cadastrarUsuarioService = cadastrarUsuarioService;
        }

        [HttpPost(Name = "Adicionar Usuario")]
        public IActionResult PostCadastrarEmpregado([FromBody] Empregado user)
        {
            var result = _cadastrarUsuarioService.CadastrarEmpregado(user);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
