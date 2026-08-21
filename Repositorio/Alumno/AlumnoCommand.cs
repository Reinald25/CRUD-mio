using CRUD.Infraestrcture.Context;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.X509Certificates;

namespace CRUD.Repositorio.Alumno
{
    public class AlumnoCommand
    {
        private readonly alumnosContext _context;

        public AlumnoCommand(alumnosContext context)
        {
            _context = context;

        }

        public async Task<Persona> CreatePersonasAsync(PersonalDataAttribute dto)
        {
            var Persona = new PersonalDataAttribute();
            return Persona;
        }

        public async Task<Persona> UpdatePersonasAsync(Persona dto)
        {
            var entity = await _context.Persona.FindAsync(StoreOptions.Idpersona)
                ?? throw new InvalidOperationException($"persona{dto.Idpersona} no encontrada");
            entity.Nombres = dto.Nombres;
            entity.Apellidos = dto.Apellidos;
            entity.Cedula = dto.Cedula;

            await _context.SaveChangesAsync();
            return new Persona
            {
                Idpersona = entity.Idpersona,
                Apellidos = entity.Apellidos,
                Nombres = entity.Nombres,
                Cedula = entity.Cedula,
                Activo = entity.Activo,

            };
            public async Task<bool> DeletePersonaAsync(int id)
        {
            var entity = await _context.Person.FindAsync(id);

        }
        }
    }
}
