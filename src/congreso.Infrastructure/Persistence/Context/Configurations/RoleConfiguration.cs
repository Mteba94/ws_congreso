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
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idRole");

            builder.Property(x => x.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50);

            builder.Property(x => x.Descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(100);

            builder.Property(x => x.Estado)
                .HasColumnName("estado"); // 1: Activo, 0: Inactivo

        }
    }
}
