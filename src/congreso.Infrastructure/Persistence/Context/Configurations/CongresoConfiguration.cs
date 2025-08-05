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
    internal sealed class CongresoConfiguration : IEntityTypeConfiguration<Congreso>
    {
        public void Configure(EntityTypeBuilder<Congreso> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("idCongreso");

            builder.Property(c => c.Nombre)
                .HasColumnName("nombre")
                .IsRequired();

            builder.Property(c => c.FechaInicio)
                .HasColumnName("fechaInicio")
                .IsRequired();

            builder.Property(c => c.FechaFin)
                .HasColumnName("fechaFin")
                .IsRequired();
        }
    }
}
