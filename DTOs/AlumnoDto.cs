using System.ComponentModel.DataAnnotations;

namespace CRUD.DTOs
{
    public class PersonaDto
    {
        // Id opcional para update; en create se ignora
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        [StringLength(50)]
        public string Estado { get; set; }
    }
}
