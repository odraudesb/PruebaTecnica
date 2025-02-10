using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_Tencica_Core.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? NumeroIdentificacion { get; set; }
        public string? Email { get; set; }
        public string? TipoIdentificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
