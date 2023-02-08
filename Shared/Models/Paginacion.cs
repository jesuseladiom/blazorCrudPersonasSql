using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorCrudPersonasSql.Shared.Models
{
    public class Paginacion
    {
        public int Pagina { get; set; } = 1;
        public int CantidadAMostrar { get; set; } = 10;
    }
}
