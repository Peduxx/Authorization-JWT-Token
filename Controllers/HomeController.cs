using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Authorization_JWT_Token.Model;
using Authorization_JWT_Token.Repositories;
using Authorization_JWT_Token.Services;

namespace Authorization_JWT_Token.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            // Recupera o usuário
            var user = UserRepository.Get(model.Username, model.Password);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonymous";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Authenticated - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "admin, manager, employee")]
        public string Employee() => "Employee";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "admin, manager")]
        public string Manager() => "Manager";

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public string Admin() => "Admin";
    }
}