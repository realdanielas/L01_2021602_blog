using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2021_602.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly usuariosContext _usuariosContext;

        public usuariosController(usuariosContext usuariosContext)
        {
            _usuariosContext = usuariosContext;
        }

        // Método para obtener todos los usuarios
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<Usuario> ListadoUsuario = (from u in _usuariosContext.usuario
                                            select u).ToList(); 
            if (ListadoUsuario.Count == 0)
            {
                return NotFound();
            }
            return Ok(ListadoUsuario);
        }

        // Método para obtener un usuario por ID
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Usuario? usuario = (from u in _usuariosContext.usuario
                                where u.usuarioId == id
                                select u).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // Método para filtrar usuarios por nombre y apellido
        [HttpGet]
        [Route("FindByNombreApellido/{nombre}/{apellido}")]
        public IActionResult FindByNombreApellido(string nombre, string apellido)
        {
            List<Usuario> usuarios = (from u in _usuariosContext.usuario
                                      where u.nombre == nombre
                                      where u.apellido == apellido
                                      select u).ToList();  
            if (usuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(usuarios);
        }

        //Método para guardar un nuevo registro
        [HttpGet]
        [Route("Add")]

        public IActionResult GuardarUsuario([FromBody] Usuario usuarios)
        {
            try
            {
                _usuariosContext.usuario.Add(usuarios);
                _usuariosContext.SaveChanges();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar registros en una tabla.
        [HttpGet]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarUsuario(int id, [FromBody] Usuario usuariosMod)
        {
            Usuario? usuarioActual = (from u in _usuariosContext.usuario
                                     where u.usuarioId == id
                                     select u).FirstOrDefault();
            if (usuarioActual == null)
            { return NotFound(); }

            usuarioActual.usuarioId = usuariosMod.usuarioId;
            usuarioActual.rolId = usuariosMod.rolId;
            usuarioActual.nombreUsuario = usuariosMod.nombreUsuario;
            usuarioActual.clave = usuariosMod.clave;
            usuarioActual.nombre = usuariosMod.nombre;
            usuarioActual.apellido = usuariosMod.apellido;

            _usuariosContext.Entry(usuarioActual).State = EntityState.Modified;
            _usuariosContext.SaveChanges();

            return Ok(usuariosMod);
        }

        // Método para eliminar
        [HttpGet]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id)
        {
            Usuario? equipo = (from u in _usuariosContext.usuario
                               where u.usuarioId == id
                               select u).FirstOrDefault();
            if (equipo == null)
                return NotFound();

            _usuariosContext.usuario.Attach(equipo);
            _usuariosContext.usuario.Remove(equipo);
            _usuariosContext.SaveChanges();

            return Ok(equipo);
        }
    }
}
