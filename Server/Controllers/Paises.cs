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
            return await context.Paises.OrderBy(x => x.Nombre).ToListAsync();
        }

        [HttpGet("{paisId}/estados")]
        public async Task<List<Estado>> GetEstados(int paisId)
        {
            return await context.Estados.Where(x => x.PaisId == paisId)
                .OrderBy(x => x.Nombre).ToListAsync();
        }
    }
}
