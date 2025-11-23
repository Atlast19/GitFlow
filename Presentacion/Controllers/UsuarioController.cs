using Aplicacion.DTOs;
using Aplicacion.Service;
using Dominio.Entidates;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Route("api/Usuarios")]
    [Produces("application/json")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllUsuarios() 
        {
            try
            {
                var list = _service.GetAllRegistros().ToList();
                return Ok(list);
            }
            catch (Exception ex) 
            {
                return BadRequest(new {Error = ex.Message});
            }
        }
        [HttpGet("GetById")]
        public UsuarioDTO GetUsuarioByID(int ID) 
        {
            return _service.GetRegistrosByID(ID);
        }
        [HttpPost]
        public IActionResult PostUsuarios([FromBody]UsuarioDTO model) 
        {
            try
            {
                _service.PostRegistros(model);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(new { Error = ex.Message});
            }
        }
        [HttpPut("Update")]
        public IActionResult PutUsuarios([FromBody]UsuarioDTO model) 
        {
            try
            {
                _service.PutRegistros(model);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(new {Error = ex.Message});
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteUsuarioByID(int ID) 
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


    }
}
