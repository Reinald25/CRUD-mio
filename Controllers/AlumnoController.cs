using CRUD.Repositorio.Alumno;
using




using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CRUD.Repositorio.Alumnos;
using CRUD.Entidades;
namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/personas")]
    [Authorize]
    public class AlumnoController : ControllerBase
    {
        private readonly AlumnoQuery _personas;
        private readonly AlumnoCommand _persona;
        [HttpGet]
    {
        public async Task<ActionResult<Alumnos>> CreatePersona([FromBody] Persona dto)

            var result = await _persona

    
        
}
