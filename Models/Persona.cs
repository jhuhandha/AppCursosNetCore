using System;
using System.Collections.Generic;

namespace AppWebSena.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Gusto = new HashSet<Gusto>();
        }

        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Gusto> Gusto { get; set; }
    }
}
