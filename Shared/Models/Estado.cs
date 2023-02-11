using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrudPersonasSql.Shared.Models
{
    public class Estado
    {
        public int Id { get; set; } 
        public string Nombre { get; set; }
        public int PaisId { get; set; }
        public Pais Pais { get; set; }
    }
}
