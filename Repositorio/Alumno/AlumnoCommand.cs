using CRUD.Infraestrcture.Context;
using CRUD.Entidades;

namespace CRUD.Repositorio.Alumno
{
    public class AlumnoCommand
    {
        private readonly alumnosContext _context;

        public AlumnoCommand(alumnosContext context)
        {
            _context = context;
        }

        public async Task<Persona> CreatePersonasAsync(Persona dto)
        {
            var entity = new Persona
            {
                Nombres = dto.Nombres,
                Apellido = dto.Apellido,
                Edad = dto.Edad,
                Estado = dto.Estado
            };

            _context.Persona.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Persona> UpdatePersonasAsync(Persona dto)
        {
            var entity = await _context.Persona.FindAsync(dto.IdPersona)
                ?? throw new InvalidOperationException($"Persona {dto.IdPersona} no encontrada");

            entity.Nombres = dto.Nombres;
            entity.Apellido = dto.Apellido;
            entity.Edad = dto.Edad;
            entity.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeletePersonaAsync(int id)
        {
            var entity = await _context.Persona.FindAsync(id);
            if (entity == null) return false;
            _context.Persona.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
