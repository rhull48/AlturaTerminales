namespace AlturaTerminales.Models
{
    public class AlturasList
    {
        public long Id { get; set; }
        public int idTerminal {get;set;}
        public string? Terminal { get; set; }
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }
        public string? Area { get; set; }
        public string? StudSize { get; set; }
        public decimal? MinOD { get; set; }
        public decimal? MaxOD { get; set; }
        public long MinGA { get; set; }
        public long MaxGA { get; set; }
        public decimal? Ajuste { get; set; }
        public decimal? Dimension { get; set; }
    }
}
