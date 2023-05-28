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
    public class RemoveMidiaController : ControllerBase
    {
        private readonly RemoveMidiaService _removeMidiaService;

        public RemoveMidiaController(RemoveMidiaService removeMidiaService)
        {
            _removeMidiaService = removeMidiaService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _removeMidiaService.RemoveMedia(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir a mídia: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }
    }
}
