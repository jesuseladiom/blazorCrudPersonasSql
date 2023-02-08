using BlazorCrudPersonasSql.Server.Helpers;
using BlazorCrudPersonasSql.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrudPersonasSql.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonasController:ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PersonasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Persona>>> Get([FromQuery]Paginacion paginacion)
        {
            var queryable= context.Personas.AsQueryable();  //asi tenemos un queryble para cada tabla
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadAMostrar);
            // paginar
            //return await queryable.Paginar(paginacion).ToListAsync();

            //meti este codigo porque no me dejo paginar
            int skip = (paginacion.Pagina - 1) * paginacion.CantidadAMostrar;
            List<Persona> res = new List<Persona>();
            int i = 0; int cantidad = 0;
            foreach (Persona persona in queryable)
            {
                i++;
                if (i > skip)
                {
                    cantidad++;
                    res.Add(persona);
                    if (cantidad >= paginacion.CantidadAMostrar) break;
                }
            }
            return  res.ToList();
            //return await context.Personas.ToListAsync(); //sin paginar
        }

        [HttpGet("{id}", Name = "obtenerPersona")]
        [AllowAnonymous]
        public async Task<ActionResult<Persona>> Get(int id)
        {
            return await context.Personas.FirstOrDefaultAsync(x => x.Id == id);
         }

        [HttpPost]
        public async Task<ActionResult> Post(Persona persona)
        {
            context.Add(persona);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("obtenerPersona", new { id = persona.Id}, persona);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Persona persona)
        {
            context.Entry(persona).State= EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();    //respuesta 204   
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Borrar(int id)
        {
            var persona= new Persona { Id = id};
            context.Remove(persona);
            await context.SaveChangesAsync();
            return NoContent();    //respuesta 204   
        }

    }
}
