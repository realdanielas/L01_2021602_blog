using Microsoft.EntityFrameworkCore;

namespace L01_2021_602.Models
{
    public class comentariosContext : DbContext
    {
        public comentariosContext(DbContextOptions<comentariosContext> options) : base(options)
        {

        }

        public DbSet<Comentario> comentario { get; set; }
    }

}
