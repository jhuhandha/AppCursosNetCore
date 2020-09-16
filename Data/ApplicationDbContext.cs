using System;
using System.Collections.Generic;
using System.Text;
using AppWebSena.Models;
using AppWebSena.Areas.Cursos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppWebSena.Areas.Programacion.Models;

namespace AppWebSena.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categoria { set; get; }
        public DbSet<Curso> Curso { set; get; }
        public DbSet<ProgramacionCurso> ProgramacionCurso { set; get; }
    }
}
