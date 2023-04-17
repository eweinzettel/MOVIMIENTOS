using System;
using System.Collections.Generic;

namespace MovimeintoWebAPI.Models
{
    public partial class Central
    {
        public int IdCentral { get; set; }
        public string CentralIndicativo { get; set; } = null!;
        public string CentralNombre { get; set; } = null!;
    }
}
