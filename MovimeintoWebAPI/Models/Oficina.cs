using System;
using System.Collections.Generic;

namespace MovimeintoWebAPI.Models
{
    public partial class Oficina
    {
        public int IdOficina { get; set; }
        public int CentralId { get; set; }
        public string OficinaTipo { get; set; } = null!;
        public string OficinaIndicativo { get; set; } = null!;
        public string OficinaNombre { get; set; } = null!;
        public virtual Central Central { get; set; } = null!;
    }
}
