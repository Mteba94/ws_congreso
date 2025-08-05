using congreso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congreso.Infrastructure.Persistence.Context.Configurations
{
    internal sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("idTag")
                .IsRequired();

            builder.Property(t => t.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Estado)
                .HasColumnName("estado")
                .IsRequired();
        }
    }
}
