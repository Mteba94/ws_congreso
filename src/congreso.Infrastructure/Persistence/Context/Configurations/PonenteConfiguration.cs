using congreso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congreso.Infrastructure.Persistence.Context.Configurations
{
    internal sealed class PonenteConfiguration : IEntityTypeConfiguration<Ponente>
    {
        public void Configure(EntityTypeBuilder<Ponente> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("idPonente")
                .IsRequired();

            builder.Property(p => p.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Apellido)
                .HasColumnName("apellido")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Titulo)
                .HasColumnName("titulo")
                .HasMaxLength(200);

            builder.Property(p => p.Empresa)
                .HasColumnName("empresa")
                .HasMaxLength(200);

            builder.Property(p => p.Biografia)
                .HasColumnName("bio")
                .HasMaxLength(1000);

            builder.Property(p => p.Foto)
                .HasColumnName("image")
                .HasMaxLength(500);
        }
    }
}
