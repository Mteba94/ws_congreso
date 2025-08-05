namespace congreso.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int usuarioCreacion { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public int? usuarioModificacion { get; set; }
        public DateTime? fechaEliminacion { get; set; }
        public int? usuarioEliminacion { get; set; }
    }
}
