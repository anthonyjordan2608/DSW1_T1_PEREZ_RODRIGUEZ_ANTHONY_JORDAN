using DSW1_T1_PEREZ_RODRIGUEZ_ANTHONY_JORDAN.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DSW1_T1_PEREZ_RODRIGUEZ_ANTHONY_JORDAN.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<NivelAcademico> NivelAcademicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NivelAcademico>()
                .HasMany(n => n.Cursos)
                .WithOne(c => c.NivelAcademico)
                .HasForeignKey(c => c.NivelAcademicoId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
