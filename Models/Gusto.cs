using System;
using System.Collections.Generic;

namespace AppWebSena.Models
{
    public partial class Gusto
    {
        public int GustoId { get; set; }
        public string Nombre { get; set; }
        public int? PersonaId { get; set; }

        public virtual Persona Persona { get; set; }
    }
}
