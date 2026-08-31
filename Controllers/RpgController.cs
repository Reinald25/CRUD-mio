using CRUD.Repositorio.Rpg;
using Microsoft.AspNetCore.Mvc;
using CRUD.Entidades;
using CRUD.DTOs;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/rpg")]
    public class RpgController : ControllerBase
    {
        private readonly RpgQuery _query;
        private readonly RpgCommand _command;

        public RpgController(RpgQuery query, RpgCommand command)
        {
            _query = query;
            _command = command;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rankings>>> GetAll()
        {
            var list = await _query.GetRankingsAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Rankings>> GetById(int id)
        {
            var entity = await _query.GetRankingByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Rankings>> Create([FromBody] RankingDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new Rankings
            {
                Usuario = dto.Usuario,
                Puntaje = dto.Puntaje,
                Ultimonivel = dto.Ultimonivel
            };

            var created = await _command.CreateRankingAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.IdRanking }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Rankings>> Update(int id, [FromBody] RankingDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = new Rankings
            {
                IdRanking = id,
                Usuario = dto.Usuario,
                Puntaje = dto.Puntaje,
                Ultimonivel = dto.Ultimonivel
            };

            try
            {
                var updated = await _command.UpdateRankingAsync(entity);
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
            var deleted = await _command.DeleteRankingAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
