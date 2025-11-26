using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSW1_T1_PEREZ_RODRIGUEZ_ANTHONY_JORDAN.Models
{
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }

        [Required]
        [StringLength(20)]
        public string CodigoCurso { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCurso { get; set; }

        [Required]
        public int Creditos { get; set; }

        [Required]
        public int HorasSemanales { get; set; }

        [Required]
        public int NivelAcademicoId { get; set; }

        [ForeignKey("NivelAcademicoId")]
        public virtual NivelAcademico NivelAcademico { get; set; }
    }
}
