namespace congreso.Domain.Entities
{
    public class TipoActividad
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int Estado { get; set; } // 1: Activo, 0: Inactivo
    }
}
