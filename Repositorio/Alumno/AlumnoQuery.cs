using CRUD.Entidades;
using CRUD.Infraestrcture.Context;

namespace CRUD.Repositorio.Alumno
{
    public class AlumnoQuery
    {
        private readonly alumnosContext _context;

        public AlumnoQuery(alumnosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alumnos>> GetPersonasAsync()
        {
            var personas = await _context.Alumnos.ToListAsync();
            return personas;
        }
    }
}
