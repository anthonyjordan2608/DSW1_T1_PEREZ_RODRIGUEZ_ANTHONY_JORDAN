using DSW1_T1_PEREZ_RODRIGUEZ_ANTHONY_JORDAN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DSW1_T1_PEREZ_RODRIGUEZ_ANTHONY_JORDAN.DTOs
{
    public class CursoCreateDto
    {
        public string CodigoCurso { get; set; }
        public string NombreCurso { get; set; }
        public int Creditos { get; set; }
        public int HorasSemanales { get; set; }
        public int NivelAcademicoId { get; set; }
        }


     }
