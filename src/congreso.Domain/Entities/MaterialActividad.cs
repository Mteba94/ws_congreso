namespace congreso.Domain.Entities
{
    public class MaterialActividad : CatalogoEntity
    {
        public int ActividadId { get; set; }
        public string MaterialDesc { get; set; } = null!;

        public Actividad Actividad { get; set; } = null!;
    }
}
