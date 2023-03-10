using BlazorCrudPersonasSql.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrudPersonasSql.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PaisesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pais>>> Get()
        {
            return await context.Pais.OrderBy(x => x.Nombre).ToListAsync();
        }

        [HttpGet("{paisId}/estados")]
        public async Task<List<Estado>> GetEstados(int paisId)
        {
            return await context.Estado.Where(x => x.PaisId == paisId)
                .OrderBy(x => x.Nombre).ToListAsync();
        }

        [HttpGet("{estadoId}/estado")]
        public async Task<List<Estado>> GetEstado(int estadoId)
        {
            return await context.Estado.Where(x => x.Id == estadoId)
                .OrderBy(x => x.Nombre).ToListAsync();
        }
    }
}
