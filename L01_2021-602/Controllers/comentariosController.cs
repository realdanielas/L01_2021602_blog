using L01_2021_602.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly comentariosContext _comentariosContext;

        public comentariosController(comentariosContext comentariosContext)
        {
            _comentariosContext = comentariosContext;
        }

        // Método para obtener todos los comentarios
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<Comentario> comentarios = (from co in _comentariosContext.comentario
                                            select co).ToList();
            if (comentarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(comentarios);
        }

        // Método para obtener un comentario por ID
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Comentario? comentario = (from co in _comentariosContext.comentario
                                     where co.comentarioId == id
                                     select co).FirstOrDefault();
            if (comentario == null)
            {
                return NotFound();
            }
            return Ok(comentario);
        }

        // Método para filtrar comentarios por un usuario específico
        [HttpGet]
        [Route("FindByUsuario/{usuarioId}")]
        public IActionResult FindByUsuario(int usuarioId)
        {
            List<Comentario> comentarios = (from co in _comentariosContext.comentario
                                            where co.usuarioId == usuarioId
                                            select co).ToList(); 
            if (comentarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(comentarios);
        }

        // Método para agregar un nuevo comentario
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] Comentario comentario)
        {
            try
            {
                _comentariosContext.comentario.Add(comentario);
                _comentariosContext.SaveChanges();
                return Ok(comentario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar un comentario
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] Comentario comentarioMod)
        {
            Comentario? comentario = (from co in _comentariosContext.comentario
                                     where co.comentarioId == id
                                     select co).FirstOrDefault();
            if (comentario == null)
            {
                return NotFound();
            }

            comentario.comentarioId = comentarioMod.comentarioId;
            comentario.publicacionId = comentarioMod.publicacionId;
            comentario.usuarioId = comentarioMod.usuarioId;
            comentario.comentario = comentarioMod.comentario;

            _comentariosContext.Entry(comentario).State = EntityState.Modified;
            _comentariosContext.SaveChanges();

            return Ok(comentarioMod);
        }

        // Método para eliminar un comentario
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            Comentario? comentario = (from co in _comentariosContext.comentario
                                     where co.comentarioId == id
                                     select co).FirstOrDefault();
            if (comentario == null)
            {
                return NotFound();
            }

            _comentariosContext.comentario.Remove(comentario);
            _comentariosContext.SaveChanges();

            return Ok(comentario);
        }
    }
}
