using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_Tencica_Core.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? Pass { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
