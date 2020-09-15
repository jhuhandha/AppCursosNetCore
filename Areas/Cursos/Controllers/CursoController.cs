using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppWebSena.Areas.Cursos.Models;
using AppWebSena.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using AppWebSena.Models.Paginador;

namespace AppWebSena.Areas.Cursos.Controllers
{
    [Area("Cursos")]
    public class CursoController : Controller
    {
        public ApplicationDbContext _dbContext;

        public CursoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index(int Pag, int Registros, string Search)
        {
            List<Curso> cursos = null;
            if(Search != null)
            {
                cursos = _dbContext.Curso.Include(e => e.Categoria)
                    .Where(e => e.NombreCurso.Contains(Search) || e.Categoria.NombreCategoria.Contains(Search)).ToList();
            }
            else
            {
                cursos = _dbContext.Curso.Include(e => e.Categoria).ToList();
            }

            string host = Request.Scheme + "://" + Request.Host.Value;
            object[] resultado = new Paginador<Curso>().paginador(cursos, Pag, Registros, "Cursos", "Curso", "Index", host);

            DataPaginador<Curso> modelo = new DataPaginador<Curso>
            {
                Lista = (List<Curso>)resultado[2],
                Pagi_info = (string)resultado[0],
                Pagi_navegacion = (string)resultado[1]
            }; 

            return View(modelo);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            Curso curso = new Curso();
            curso.Categorias = _dbContext.Categoria.ToList();
            return View(curso);
        }

        [HttpPost]
        public IActionResult Guardar(Curso curso)
        {
            if (!ModelState.IsValid)
            {
                curso.Categorias = _dbContext.Categoria.ToList();
                return View(curso.CursoId == 0 ? "Crear" : "Editar", curso);
            }

            if (curso.CursoId == 0)
            {
                if(curso.ImagenCarga != null)
                {
                    string[] ext = curso.ImagenCarga.FileName.Split(".");
                    string nuevo_nombre = curso.NombreCurso + "." + ext[ext.Length - 1];
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", nuevo_nombre);
                    using (var stream = System.IO.File.Create(path))
                    {
                        curso.ImagenCarga.CopyTo(stream);
                        curso.Imagen = nuevo_nombre;
                    }
                }
                else
                {
                    curso.Imagen = "default.png";
                }

                _dbContext.Add(curso);
            }
            else
            {
                _dbContext.Update(curso);
            }

            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            Curso curso = _dbContext.Curso.Find(id);
            if (curso == null)
            {
                return RedirectToAction("Index");
            }
            curso.Categorias = _dbContext.Categoria.ToList();

            return View(curso);
        }

        public IActionResult CambiarEstado(int id)
        {
            Curso curso = _dbContext.Curso.Find(id);
            if (curso == null)
            {
                return RedirectToAction("Index");
            }

            curso.Estado = !curso.Estado;

            _dbContext.Update(curso);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        //public IActionResult Eliminar(int id)
        //{
        //    Curso curso = _dbContext.Curso.Find(id);
        //    if(curso== null)
        //    {

        //    }

        //    _dbContext.Curso.Remove(curso);
        //    _dbContext.SaveChanges();
        //}

    }


}
