using congreso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congreso.Infrastructure.Persistence.Context.Configurations
{
    internal sealed class TipoIdentificacionConfiguration : IEntityTypeConfiguration<TipoIdentificacion>
    {
        public void Configure(EntityTypeBuilder<TipoIdentificacion> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idTipoIdentificacion");

            builder.Property(x => x.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(500);

            builder.Property(x => x.Estado)
                .HasColumnName("estado")
                .IsRequired(); // Default value for Estado is 1 (Activo)
        }
    }
}
