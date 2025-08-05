using congreso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace congreso.Infrastructure.Persistence.Context.Configurations
{
    internal sealed class InscripcionConfiguration : IEntityTypeConfiguration<Inscripcion>
    {
        public void Configure(EntityTypeBuilder<Inscripcion> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idInscripcion");

            builder.Property(x => x.UserId)
                .HasColumnName("idUsuario")
                .IsRequired();

            builder.Property(x => x.ActividadId)
                .HasColumnName("idActividad")
                .IsRequired();

            builder.Property(x => x.FechaInscripcion)
                .HasColumnName("fechaInscripcion")
                .IsRequired();
        }
    }
}
