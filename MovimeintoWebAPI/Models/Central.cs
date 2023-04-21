using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MovimeintoWebAPI.Models
{
    public partial class Central
    {
        public Central()
        {
            Oficinas = new HashSet<Oficina>();
        }

        public int IdCentral { get; set; }
        public string CentralIndicativo { get; set; } = null!;
        public string CentralNombre { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Oficina> Oficinas { get; set; }
    }
}
