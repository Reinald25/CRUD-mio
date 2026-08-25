using CRUD.Entidades;
using CRUD.Infraestrcture.Context;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Repositorio.Alumno
{
    public class AlumnoQuery
    {
        private readonly alumnosContext _context;

        public AlumnoQuery(alumnosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Persona>> GetPersonasAsync()
        {
            var personas = await _context.Persona.ToListAsync();
            return personas;
        }

        public async Task<Persona?> GetPersonaByIdAsync(int id)
        {
            return await _context.Persona.FindAsync(id);
        }
    }
}
