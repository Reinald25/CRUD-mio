using CRUD.Entidades;
using CRUD.Infraestrcture.Context;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Repositorio.Rpg
{
    public class RpgQuery
    {
        private readonly rpgContext _context;

        public RpgQuery(rpgContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rankings>> GetRankingsAsync()
        {
            var list = await _context.Rankings.ToListAsync();
            return list;
        }

        public async Task<Rankings?> GetRankingByIdAsync(int id)
        {
            return await _context.Rankings.FindAsync(id);
        }
    }
}
