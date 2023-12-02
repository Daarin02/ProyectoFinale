using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TomChino_ComidaChina.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace TomChino_ComidaChina.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public CuentasController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("registro")]
        public async Task<ActionResult<UserToken>> RegistroUsuario([FromBody] User userInfo)
        {
            var user = new IdentityUser
            { UserName = userInfo.Username };

            var result = await userManager.CreateAsync(user, userInfo.Password);

            if (result.Succeeded)
            {
                return BuildToken(userInfo);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        private UserToken BuildToken(User userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.Username),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["jwtkey"]));

            var credential = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1600);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credential);

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler()
                .WriteToken(token),
                Expiration = expiration
            };
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserToken>> Login([FromBody] User model)
        {
            var result = await signInManager.PasswordSignInAsync(
                model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return BuildToken(model);
            }
            else
            {
                return BadRequest("Login fallido");
            }
        }

        [HttpPost("CambiarContrasena")]

        public async Task<ActionResult<UserToken>> Cambiarcontrasena([FromBody] User model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            var removePasswordResult = await userManager.RemovePasswordAsync(user);

            if (!removePasswordResult.Succeeded)
            {
                return BadRequest("Error al eliminar la contraseña actual");
            }

            // Agregar la nueva contraseña
            var addPasswordResult = await userManager.AddPasswordAsync(user, model.Password);

            if (addPasswordResult.Succeeded)
            {
                return Ok("Contraseña cambiada con éxito");
            }
            else
            {
                return BadRequest("Error al agregar la nueva contraseña");
            }
        }
    }
}

