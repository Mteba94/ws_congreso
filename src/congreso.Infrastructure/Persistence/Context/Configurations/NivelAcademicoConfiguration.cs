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
    internal sealed class NivelAcademicoConfiguration : IEntityTypeConfiguration<NivelAcademico>
    {
        public void Configure(EntityTypeBuilder<NivelAcademico> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Id)
                .HasColumnName("nivelAcademicoId");

            builder.Property(n => n.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            builder.Property(n => n.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");

            builder.Property(n => n.Estado)
                .HasColumnName("estado"); // 1: Activo, 0: Inactivo
        }
    }
}
