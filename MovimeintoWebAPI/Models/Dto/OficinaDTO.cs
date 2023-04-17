namespace MovimeintoWebAPI.Models.Dto
{
    public class OficinaDTO
    {
        public int IdOficina { get; set; }
        public int CentralId { get; set; }
        public string CentralIndicativo { get; set; } = null!;
        public string CentralNombre { get; set; } = null!;
        public string OficinaTipo { get; set; } = null!;
        public string OficinaIndicativo { get; set; } = null!;
        public string OficinaNombre { get; set; } = null!;
    }
}
