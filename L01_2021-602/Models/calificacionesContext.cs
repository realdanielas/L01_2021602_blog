using Microsoft.EntityFrameworkCore;

namespace L01_2021_602.Models
{
    public class calificacionesContext : DbContext
    {
        public calificacionesContext(DbContextOptions<calificacionesContext> options) : base(options)
        {

        }

        public DbSet<Calificaciones> calificaciones { get; set; }
    }
}
