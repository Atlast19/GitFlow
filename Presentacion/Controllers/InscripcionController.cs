using Aplicacion.DTOs;
using Aplicacion.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("api/Inscripcoines")]
    [Produces("application/json")]
    [ApiController]
    public class InscripcionController : Controller
    {
        private readonly InscripcionService _service;
        
        public InscripcionController(InscripcionService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public IActionResult GetInscripciones()
        {
            try
            {
                var registros =_service.GetAllRegistros();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                // Aquí llega la excepción del repositorio
                return BadRequest(new { Error = ex.Message});
            }
        }

        [HttpGet("GetByID")]
        public InscripcionesDTO GetInscripcionesByID(int ID)
        {
            return _service.GetRegistrosByID(ID);
        }
        [HttpPost]
        public IActionResult PostInscripciones([FromBody]InscripcionesDTO model)
        {
            try
            {
                _service.PostRegistros(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest( new {Error = ex.Message});
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteInscipcionesByID(int ID)
        {
            try
            {
                _service.DeleteById(ID);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(new { Error = ex.Message});
            }

        }
        [HttpPut("Put")]
        public IActionResult PutInscripciones([FromBody] InscripcionesDTO model)
        {
            try
            {
                _service.PutRegistros(model);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(new { Error = ex.Message});
            }
        }
    }
}


