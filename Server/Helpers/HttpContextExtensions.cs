using Microsoft.EntityFrameworkCore;

namespace BlazorCrudPersonasSql.Server.Helpers
{
    public static class HttpContextExtensions
    {
        // se usa IQueryable para que funcione con cualquier consulta y tabla
        public static async Task InsertarParametrosPaginacionEnRespuesta<T>(this HttpContext context,
            IQueryable<T> queryable, int cantidadRegistrosAMostrar)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            double conteo = await queryable.CountAsync();  //cantidad de regs en la tabla
            double totalPaginas = Math.Ceiling(conteo / cantidadRegistrosAMostrar);
            context.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
            // pudieramos agregar el total de registros si se desea tmb
        }
    }
}
