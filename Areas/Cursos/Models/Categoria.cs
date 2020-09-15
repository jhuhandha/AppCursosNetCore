using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppWebSena.Areas.Cursos.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { set; get; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string NombreCategoria { set; get; }

        public ICollection<Curso> Cursos { set; get; }
    }
}
