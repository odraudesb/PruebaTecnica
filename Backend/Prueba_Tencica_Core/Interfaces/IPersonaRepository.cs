using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prueba_Tencica_Core.Models;

namespace Prueba_Tencica_Core.Interfaces
{
    public interface IPersonaRepository
    {
        Task<IEnumerable<Persona>> GetAll();
        Task<Persona> GetById(int id);
        Task Add(Persona persona);
        Task Update(Persona persona);
        Task Delete(int id);

    }
}
