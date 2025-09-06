namespace congreso.Domain.Entities
{
    public class TipoActividad : CatalogoEntity
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
