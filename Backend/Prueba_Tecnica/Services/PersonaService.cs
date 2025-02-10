using Prueba_Tencica_Core.Interfaces;
using Prueba_Tencica_Core.Models;

namespace Prueba_Tecnica.Services
{
    public class PersonaService
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<IEnumerable<Persona>> GetAllPersonasAsync()
        {
            try
            {
                return await _personaRepository.GetAll();
            }
            catch (Exception ex)
            {
                // Loggeamos el error
                // _logger.LogError(ex, "Error al obtener todas las personas.");
                throw new Exception("Error al obtener las personas", ex);
            }
        }

        public async Task<Persona> GetPersonaByIdAsync(int id)
        {
            try
            {
                return await _personaRepository.GetById(id);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error al obtener la persona por ID.");
                throw new Exception($"Error al obtener la persona con ID {id}", ex);
            }
        }

        public async Task AddPersonaAsync(Persona persona)
        {
            try
            {
                await _personaRepository.Add(persona);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error al agregar una persona.");
                throw new Exception("Error al agregar la persona", ex);
            }
        }

        public async Task UpdatePersonaAsync(Persona persona)
        {
            try
            {
                await _personaRepository.Update(persona);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error al actualizar la persona.");
                throw new Exception("Error al actualizar la persona", ex);
            }
        }

        public async Task DeletePersonaAsync(int id)
        {
            try
            {
                await _personaRepository.Delete(id);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error al eliminar la persona.");
                throw new Exception($"Error al eliminar la persona con ID {id}", ex);
            }
        }
    }
}
