using CRUD.Repositorio.Alumno;
using Microsoft.AspNetCore.Mvc;
using CRUD.Entidades;
using CRUD.DTOs;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/alumnos")]
    public class AlumnoController : ControllerBase
    {
        private readonly AlumnoQuery _query;
        private readonly AlumnoCommand _command;

        public AlumnoController(AlumnoQuery query, AlumnoCommand command)
        {
            _query = query;
            _command = command;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetAll()
        {
            var list = await _query.GetPersonasAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Persona>> GetById(int id)
        {
            var entity = await _query.GetPersonaByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> Create([FromBody] PersonaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new Persona
            {
                Nombres = dto.Nombres,
                Apellido = dto.Apellido,
                Edad = dto.Edad,
                Estado = dto.Estado
            };

            var created = await _command.CreatePersonasAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.IdPersona }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Persona>> Update(int id, [FromBody] PersonaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new Persona
            {
                IdPersona = id,
                Nombres = dto.Nombres,
                Apellido = dto.Apellido,
                Edad = dto.Edad,
                Estado = dto.Estado
            };

            try
            {
                var updated = await _command.UpdatePersonasAsync(entity);
                return Ok(updated);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _command.DeletePersonaAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
