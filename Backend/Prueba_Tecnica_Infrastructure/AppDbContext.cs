using Microsoft.EntityFrameworkCore;
using Prueba_Tencica_Core.Models;

namespace Prueba_Tencica_Infrastructure
{
    public class AppDbContext : DbContext
    {
        // Constructor que pasa las opciones de configuración a la clase base
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
