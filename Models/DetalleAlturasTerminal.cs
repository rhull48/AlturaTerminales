namespace AlturaTerminales.Models
{
    public class DetalleAlturasTerminal
    {
        public int Id { get; set; }
        public string? Terminal { get; set; }
        public string? Descripcion { get; set; }
        public string? CombinacionGA { get; set; }
        public string? Sello { get; set; }
        public string? Altura { get; set; }
        public string? Ancho { get; set; }
        public int? Pull { get; set; }
        public string? Nota { get; set; }
    }
}
