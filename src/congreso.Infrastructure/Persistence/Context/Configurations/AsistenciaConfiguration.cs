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
    internal sealed class AsistenciaConfiguration : IEntityTypeConfiguration<Asistencia>
    {
        public void Configure(EntityTypeBuilder<Asistencia> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idAsistencia");

            builder.HasOne(a => a.Inscripcion)
                .WithMany(i => i.Asistencias)
                .HasForeignKey(a => a.InscripcionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
