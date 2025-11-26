using System.ComponentModel.DataAnnotations;

namespace DSW1_T1_PEREZ_RODRIGUEZ_ANTHONY_JORDAN.Models
{
    public class NivelAcademico
    {
        [Key]
        public int NivelAcademicoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public int Orden { get; set; }

        public virtual ICollection<Curso> Cursos { get; set; }
    }
}
