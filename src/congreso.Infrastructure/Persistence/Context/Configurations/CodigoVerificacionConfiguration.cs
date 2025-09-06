using congreso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congreso.Infrastructure.Persistence.Context.Configurations
{
    internal sealed class CodigoVerificacionConfiguration : IEntityTypeConfiguration<CodigoVerificacion>
    {
        public void Configure(EntityTypeBuilder<CodigoVerificacion> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idCodigoVerificacion");

            builder.Property(x => x.Codigo)
                .HasColumnName("codigo")
                .HasMaxLength(600)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.Codigo,
                x.UserId
            })
                .IsUnique();

        }
    }
}
