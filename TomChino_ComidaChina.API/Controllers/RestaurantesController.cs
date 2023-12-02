using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TomChino_ComidaChina.API.Models;

namespace TomChino_ComidaChina.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public RestaurantesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpPost("registrar")]

        public async Task<ActionResult<int>> Guardar(Restaurantes restaurante)
        {
            var nuevoRestaurante = restaurante;
            context.Add(nuevoRestaurante);

            await context.SaveChangesAsync();

            if (nuevoRestaurante.id >= 0)
            {
                return nuevoRestaurante.id;
            }
            else
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("lista")]

        public async Task<ActionResult<List<Restaurantes>>> Registros()
        {
            var lista = new List<Restaurantes>();
            lista = await context.registrosRestaurantes.ToListAsync();
            if(lista.Count > 0)
            {
                return lista;
            }
            else
            {
                return NoContent();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var orden = await context.registrosRestaurantes.FindAsync(id);
            if (orden == null)
                return NotFound();
            context.registrosRestaurantes.Remove(orden);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
