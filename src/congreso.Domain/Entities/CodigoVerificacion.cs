namespace congreso.Domain.Entities
{
    public class CodigoVerificacion : CatalogoEntity
    {
        public string Codigo { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string Purpose { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
