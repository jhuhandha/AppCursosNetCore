using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWebSena.Areas.Programacion.Models
{
    public class ProgramacionViewModel
    {
        public int ProgramacionId { set; get; }
        public DateTime Fecha { set; get; }
        public string HoraInicio { set; get; }
        public string HoraFinal { set; get; }
        public string Descripcion { set; get; }
        public int CursoId { set; get; }
        public string NombreCurso { set; get; }
    }
}
