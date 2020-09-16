using AppWebSena.Areas.Cursos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppWebSena.Areas.Programacion.Models
{
    public class ProgramacionCurso
    {
        [Key]
        public int ProgramacionId { set; get; }
        public DateTime Fecha { set; get; }
        public string HoraInicio { set; get; }
        public string HoraFinal { set; get; }
        public string Descripcion { set; get; }
        public int CursoId { set; get; }

        public Curso Curso { set; get; }
    }
}
