using System.ComponentModel.DataAnnotations;

namespace L01_2021_602.Models
{
    public class Blog
    {
    }

    public class Roles
    {
        [Key]
        public int rolId { get; set; }
        public string? rol { get; set; }
    }

    public class Usuario
    {
        [Key]
        
        public int usuarioId { get; set; }
        public int rolId { get; set; }
        public string? nombreUsuario { get; set; }
        public string? clave { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public Roles? rol { get; set; }
    }

    public class Publicaciones
    {
        [Key]
        public int publicacionId { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public int usuarioId { get; set; }
        public Usuario? usuario { get; set; }
    }

    public class Comentario
    {
        [Key]
        public int comentarioId { get; set; }
        public int publicacionId { get; set; }
        public int usuarioId { get; set; }
        public string? comentario { get; set; }
        public Usuario? usuario { get; set; }
        public Publicaciones? publicacion { get; set; }
    }

    public class Calificaciones
    {
        [Key]

        public int calificacionId { get; set; }
        public int publicacionId { get; set; }
        public int usuarioId { get; set; }
        public int calificacion { get; set; }
        public Usuario? usuario { get; set; } 
        public Publicaciones? publicacion { get; set; }
    }
}
