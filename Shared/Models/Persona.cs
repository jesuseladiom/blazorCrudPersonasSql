using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrudPersonasSql.Shared.Models
{
    public class Persona    //JEMS
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Debe Seleccionar un Estado")]
        public int EstadoId { get; set; }
    }


}
