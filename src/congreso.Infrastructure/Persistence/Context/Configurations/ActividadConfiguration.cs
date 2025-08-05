using congreso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congreso.Infrastructure.Persistence.Context.Configurations
{
    internal sealed class ActividadConfiguration : IEntityTypeConfiguration<Actividad>
    {
        public void Configure(EntityTypeBuilder<Actividad> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("idActividad");

            builder.Property(a => a.CongresoId)
                .IsRequired()
                .HasColumnName("idCongreso");

            builder.Property(a => a.Titulo)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Titulo");

            builder.Property(a => a.Descripcion)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("Descripcion");

            builder.Property(a => a.TipoActividadId)
                .IsRequired()
                .HasColumnName("idTipoActividad");

            builder.Property(a => a.FechaActividad)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("FechaActividad");

            builder.Property(a => a.HoraInicio)
                .IsRequired()
                .HasColumnName("HoraInicio");

            builder.Property(a => a.HoraFin)
                .IsRequired()
                .HasColumnName("HoraFin");

            builder.Property(a => a.CuposDisponibles)
                .IsRequired()
                .HasColumnName("CuposDisponibles");

            builder.Property(a => a.CuposTotales)
                .IsRequired()
                .HasColumnName("CuposTotales");

            builder.Property(a => a.Ubicacion)
                .HasMaxLength(200)
                .HasColumnName("Ubicacion");

            builder.Property(a => a.RequisitosPrevios)
                .HasMaxLength(500)
                .HasColumnName("RequisitosPrevios");
        }
    }
}
