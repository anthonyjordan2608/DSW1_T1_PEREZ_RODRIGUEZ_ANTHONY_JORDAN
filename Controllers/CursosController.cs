using DSW1_T1_PEREZ_RODRIGUEZ_ANTHONY_JORDAN.Data;
using DSW1_T1_PEREZ_RODRIGUEZ_ANTHONY_JORDAN.DTOs;
using DSW1_T1_PEREZ_RODRIGUEZ_ANTHONY_JORDAN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DSW1_T1_PEREZ_RODRIGUEZ_ANTHONY_JORDAN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public CursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            var cursos = await _context.Cursos
                .Include(c => c.NivelAcademico)
                .ToListAsync();
            return Ok(cursos);
        }

        [HttpGet("por-nivel/{nivelId}")]
        public async Task<ActionResult<IEnumerable<Curso>>> ListarCursosPorNivel(
            int nivelId,
            int pagina = 1,
            int tamanoPagina = 10)
        {
            if (pagina < 1) pagina = 1;
            if (tamanoPagina < 1) tamanoPagina = 10;

            var cursosQuery = _context.Cursos
                .Include(c => c.NivelAcademico)
                .Where(c => c.NivelAcademicoId == nivelId);

            var totalCursos = await cursosQuery.CountAsync();
            var cursos = await cursosQuery
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            var resultado = new
            {
                TotalCursos = totalCursos,
                Pagina = pagina,
                TamanoPagina = tamanoPagina,
                TotalPaginas = (int)Math.Ceiling(totalCursos / (double)tamanoPagina),
                Cursos = cursos
            };

            return Ok(resultado);
        }

        [HttpGet("nivel/{nivelId}")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursosPorNivel(int nivelId)
        {
            var cursos = await _context.Cursos
                .Include(c => c.NivelAcademico)
                .Where(c => c.NivelAcademicoId == nivelId)
                .ToListAsync();

            if (!cursos.Any())
                return NotFound($"No se encontraron cursos para el nivel académico ID: {nivelId}");

            return Ok(cursos);
        }

        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso([FromBody] CursoCreateDto cursoDto)
        {
            var nivelExiste = await _context.NivelAcademicos
                .AnyAsync(n => n.NivelAcademicoId == cursoDto.NivelAcademicoId);

            if (!nivelExiste)
                return BadRequest("El nivel académico especificado no existe");

            var curso = new Curso
            {
                CodigoCurso = cursoDto.CodigoCurso,
                NombreCurso = cursoDto.NombreCurso,
                Creditos = cursoDto.Creditos,
                HorasSemanales = cursoDto.HorasSemanales,
                NivelAcademicoId = cursoDto.NivelAcademicoId
            };

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCursos), new { id = curso.CursoId }, curso);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, [FromBody] Curso curso) 
        {
            if (id != curso.CursoId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nivelExiste = await _context.NivelAcademicos
                .AnyAsync(n => n.NivelAcademicoId == curso.NivelAcademicoId);

            if (!nivelExiste)
                return BadRequest("El nivel académico especificado no existe");

            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        private bool CursoExists(int id)
        {
            return _context.Cursos.Any(e => e.CursoId == id);
        }
    }
}
