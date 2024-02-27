using L01_2021_602.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021_602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly calificacionesContext _calificacionesContext;

        public calificacionesController(calificacionesContext calificacionesContext)
        {
            _calificacionesContext = calificacionesContext;
        }

        // Método para obtener todas las calificaciones
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<Calificaciones> calificaciones = (from e in _calificacionesContext.calificaciones
                                                   select e).ToList(); ;
            if (calificaciones.Count == 0)
            {
                return NotFound();
            }
            return Ok(calificaciones);
        }

        // Método para obtener una calificación por ID
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Calificaciones? calificacion = (from c in _calificacionesContext.calificaciones
                                           where c.calificacionId == id
                                           select c).FirstOrDefault();
            if (calificacion == null)
            {
                return NotFound();
            }
            return Ok(calificacion);
        }

        // Método para filtrar calificaciones por una publicación específica
        [HttpGet]
        [Route("FindByPublicacion/{publicacionId}")]
        public IActionResult FindByPublicacion(int publicacionId)
        {
            List<Calificaciones> calificaciones = (from c in _calificacionesContext.calificaciones
                                                   where c.publicacionId == publicacionId
                                                   select c).ToList();
            if (calificaciones.Count == 0)
            {
                return NotFound();
            }
            return Ok(calificaciones);
        }

        // Método para agregar una nueva calificación
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] Calificaciones calificacion)
        {
            try
            {
                _calificacionesContext.calificaciones.Add(calificacion);
                _calificacionesContext.SaveChanges();
                return Ok(calificacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para actualizar una calificación
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] Calificaciones calificacionModificar)
        {
            Calificaciones? calificacion = (from c in _calificacionesContext.calificaciones
                                           where c.calificacionId == id
                                           select c).FirstOrDefault();
            if (calificacion == null)
            {
                return NotFound();
            }

            calificacion.publicacionId = calificacionModificar.publicacionId;
            calificacion.usuarioId = calificacionModificar.usuarioId;
            calificacion.calificacion = calificacionModificar.calificacion;

            _calificacionesContext.Entry(calificacion).State = EntityState.Modified;
            _calificacionesContext.SaveChanges();

            return Ok(calificacionModificar);
        }

        // Método para eliminar una calificación
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            Calificaciones? calificacion = (from c in _calificacionesContext.calificaciones
                                           where c.calificacionId == id
                                           select c).FirstOrDefault();
            if (calificacion == null)
            {
                return NotFound();
            }

            _calificacionesContext.calificaciones.Remove(calificacion);
            _calificacionesContext.SaveChanges();

            return Ok(calificacion);
        }
    }
}
