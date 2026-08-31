using System.ComponentModel.DataAnnotations;

namespace CRUD.DTOs
{
    public class RankingDto
    {
        // Id opcional para update; en create se ignora
        public int? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Usuario { get; set; }

        [Required]
        public decimal Puntaje { get; set; }

        [Required]
        public int Ultimonivel { get; set; }
    }
}
