using Microsoft.EntityFrameworkCore;

namespace L01_2021_602.Models
{
    public class usuariosContext : DbContext
    {
        public usuariosContext(DbContextOptions<usuariosContext> options) : base(options)
        {

        }

        public DbSet<Usuario> usuario { get; set; }
    }
}
