using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AppWebSena.Areas.Programacion.Models;

namespace AppWebSena.Areas.Cursos.Models
{
    public class Curso
    {
        public int CursoId { set; get; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(80, ErrorMessage = "Debe tener como maximo 80 caracteres")]
        public string NombreCurso { set; get; }

        [Display(Name = "Descripción")]
        [Required]
        public string DescripcionCurso { set; get; }

        public string Imagen { set; get; }

        [Display(Name = "Duración (minutos)")]
        [Required]
        [Range(10, 200)]
        public int Duracion { set; get; }

        [Display(Name = "Categoria")]
        [Required]
        public int CategoriaId { set; get; }

        public bool Estado { set; get; }

        public Categoria Categoria { set; get; }

        public ICollection<ProgramacionCurso> Programaciones { set; get; }

        [NotMapped]
        public List<Categoria> Categorias { set; get; }

        [NotMapped]
        [Display(Name = "Imagen")]
        public IFormFile ImagenCarga { set; get; }
    }
}
