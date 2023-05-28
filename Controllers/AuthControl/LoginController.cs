using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend_squad1.Models;
using backend_squad1.DataModels;
using backend_squad1.Services;

namespace backend_squad1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult PostLogin([FromBody] LoginRequest login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Senha))
            {
                return BadRequest("Email e senha são obrigatórios");
            }

            var result = _loginService.Authenticate(login.Email, login.Senha);

            if (result.Success)
            {
                return Ok(new { Matricula = result.Matricula, Nome = result.Nome, Token = result.Token });
            }
            else
            {
                return BadRequest("Usuário ou senha incorretos");
            }
        }
    }
}
