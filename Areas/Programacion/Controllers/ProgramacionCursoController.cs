using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppWebSena.Areas.Cursos.Models;
using AppWebSena.Data;
using Microsoft.AspNetCore.Mvc;
using AppWebSena.Areas.Programacion.Models;
using Microsoft.EntityFrameworkCore;

namespace AppWebSena.Areas.Programacion.Controllers
{
    [Area("Programacion")]
    public class ProgramacionCursoController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ProgramacionCursoController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Curso> cursos = _dbContext.Curso.Where(e => e.Estado).ToList();
            ViewData["Curso"] = cursos;
            return View();
        }

        public IActionResult Guardar(ProgramacionCurso programacionCurso)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = false });
            }

            _dbContext.Add(programacionCurso);
            _dbContext.SaveChanges();

            return Json(new { status = true });
        }

        public IActionResult Listar()
        {
            try
            {
                List<ProgramacionViewModel> programaciones = _dbContext.ProgramacionCurso.Join(
                        _dbContext.Curso,
                        curso => curso.CursoId,
                        programacion => programacion.CursoId,
                        (programacion, curso) => new ProgramacionViewModel
                        {
                            ProgramacionId = programacion.ProgramacionId,
                            Fecha = programacion.Fecha,
                            HoraInicio = programacion.HoraInicio,
                            HoraFinal = programacion.HoraFinal,
                            Descripcion = programacion.Descripcion,
                            CursoId = curso.CursoId,
                            NombreCurso = curso.NombreCurso
                        }
                    ).ToList();
                return Json(new { status = true, data = programaciones });
            }
            catch (Exception ex)
            {
                return Json(new { status = false });
            }
        }
    }
}
