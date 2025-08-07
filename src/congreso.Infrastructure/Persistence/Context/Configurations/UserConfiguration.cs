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
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idUsuario");

            builder.Property(x => x.Pnombre)
                .HasColumnName("primerNombre")
                .HasMaxLength(50);

            builder.Property(x => x.Snombre)
                .HasColumnName("segundoNombre")
                .HasMaxLength(50);

            builder.Property(x => x.Papellido)
                .HasColumnName("primerApellido")
                .HasMaxLength(50);

            builder.Property(x => x.Sapellido)
                .HasColumnName("segundoApellido")
                .HasMaxLength(50);

            builder.Property(x => x.TipoParticipanteId)
                .HasColumnName("tipoParticipante");

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(256);

            builder.Property(x => x.Telefono)
                .HasColumnName("telefono")
                .HasMaxLength(15);

            builder.Property(x => x.FechaNacimiento)
                .HasColumnName("fechaNacimiento");

            builder.Property(x => x.TipoIdentificacionId)
                .HasColumnName("tipoIdentificacion");

            builder.Property(x => x.NumeroIdentificacion)
                .HasColumnName("numeroIdentificacion")
                .HasMaxLength(50);

            builder.Property(x => x.NivelAcademicoId)
                .HasColumnName("nivelAcademicoId")
                .HasMaxLength(50);

            builder.Property(x => x.Semestre)
                .HasColumnName("semestre");

            builder.Property(x => x.Estado)
                .HasColumnName("estado");

            builder.Property(x => x.Password)
                .HasColumnName("password")
                .HasMaxLength(400);

            builder.Property(x => x.EmailConfirmed)
                .HasColumnName("emailConfirmed");

            builder.Property(x => x.LockoutEnd)
                .HasColumnName("lockOutEnd");

            builder.Property(x => x.LockoutEnabled)
                .HasColumnName("lockOutEnabled");

            builder.Property(x => x.AccessFailedCount)
                .HasColumnName("accessFailedCount");

            builder.Property(x => x.SecurityStamp)
                .HasColumnName("securityStamp");
        }
    }
}
