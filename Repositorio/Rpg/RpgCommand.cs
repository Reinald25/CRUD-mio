using CRUD.Infraestrcture.Context;
using CRUD.Entidades;

namespace CRUD.Repositorio.Rpg
{
    public class RpgCommand
    {
        private readonly rpgContext _context;

        public RpgCommand(rpgContext context)
        {
            _context = context;
        }

        public async Task<Rankings> CreateRankingAsync(Rankings dto)
        {
            var entity = new Rankings
            {
                Usuario = dto.Usuario,
                Puntaje = dto.Puntaje,
                Ultimonivel = dto.Ultimonivel
            };

            _context.Rankings.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Rankings> UpdateRankingAsync(Rankings dto)
        {
            var entity = await _context.Rankings.FindAsync(dto.IdRanking)
                ?? throw new InvalidOperationException($"Ranking {dto.IdRanking} no encontrado");

            entity.Usuario = dto.Usuario;
            entity.Puntaje = dto.Puntaje;
            entity.Ultimonivel = dto.Ultimonivel;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteRankingAsync(int id)
        {
            var entity = await _context.Rankings.FindAsync(id);
            if (entity == null) return false;
            _context.Rankings.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
