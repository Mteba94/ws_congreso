namespace congreso.Domain.Entities
{
    public class TipoParticipante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int Estado { get; set; }
    }
}
