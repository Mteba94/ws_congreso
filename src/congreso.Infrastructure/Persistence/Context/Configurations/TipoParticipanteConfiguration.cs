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
    internal sealed class TipoParticipanteConfiguration : IEntityTypeConfiguration<TipoParticipante>
    {
        public void Configure(EntityTypeBuilder<TipoParticipante> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idTipoParticipante");

            builder.Property(x => x.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(500);

            builder.Property(x => x.Estado)
                .HasColumnName("estado")
                .IsRequired();
        }
    }
}
