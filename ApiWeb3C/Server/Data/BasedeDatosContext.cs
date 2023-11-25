using ApiWeb3C.Shared.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb3C.Server.Data
{
    public class BasedeDatosContext : DbContext
    {
        public BasedeDatosContext(DbContextOptions<BasedeDatosContext> options) : base(options)
        {

        }

        public DbSet<Jugador> Personas { get; set; }
        public DbSet<Equipo> Clasificaciones { get; set; }
        public DbSet<Habito> Habitos { get; set; }
    }
}
