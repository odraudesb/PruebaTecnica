using System;
using Microsoft.EntityFrameworkCore;
using Prueba_Tencica_Core.Interfaces;
using Prueba_Tencica_Core.Models;
using Prueba_Tencica_Infrastructure;
public class PersonaRepository : IPersonaRepository
{
    private readonly AppDbContext _context;

    public PersonaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Persona>> GetAll()
    {
        return await _context.Personas
          .FromSqlRaw("EXEC sp_ObtenerPersonas")
          .AsNoTracking()
          .ToListAsync();
    }

    public async Task<Persona> GetById(int id) => await _context.Personas.FindAsync(id);

    public async Task Add(Persona persona)
    {
        _context.Personas.Add(persona);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Persona persona)
    {
        _context.Personas.Update(persona);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var persona = await _context.Personas.FindAsync(id);
        if (persona != null)
        {
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
        }
    }
}
