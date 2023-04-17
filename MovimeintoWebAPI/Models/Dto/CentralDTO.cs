using System;
using System.Collections.Generic;


namespace MovimeintoWebAPI.Models.Dto
{
    public partial class CentralDTO
    {
        public int IdCentral { get; set; }
        public string CentralIndicativo { get; set; } = null!;
        public string CentralNombre { get; set; } = null!;
    }
}

