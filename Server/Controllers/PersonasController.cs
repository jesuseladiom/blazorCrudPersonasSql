using BlazorCrudPersonasSql.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrudPersonasSql.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonasController:ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PersonasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Persona>>> Get()
        {
            return await context.Personas.ToListAsync();
        }

        [HttpGet("{id}", Name = "obtenerPersona")]
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
