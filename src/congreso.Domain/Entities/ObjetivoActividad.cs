namespace congreso.Domain.Entities
{
    public class ObjetivoActividad : CatalogoEntity
    {
        public int ActividadId { get; set; }
        public string ObjetivoDesc { get; set; } = null!;

        public Actividad Actividad { get; set; } = null!;
    }
}
