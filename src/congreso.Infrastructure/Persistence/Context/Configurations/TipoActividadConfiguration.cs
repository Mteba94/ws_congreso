using congreso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congreso.Infrastructure.Persistence.Context.Configurations
{
    internal sealed class TipoActividadConfiguration : IEntityTypeConfiguration<TipoActividad>
    {
        public void Configure(EntityTypeBuilder<TipoActividad> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("idTipoActividad")
                .IsRequired();

            builder.Property(t => t.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(500);

            builder.Property(t => t.Estado)
                .HasColumnName("estado")
                .IsRequired()
                .HasDefaultValue(1); // 1: Activo, 0: Inactivo
        }
    }
}
