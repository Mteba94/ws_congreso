using congreso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congreso.Infrastructure.Persistence.Context.Configurations;

internal class MaterialActividadConfiguration : IEntityTypeConfiguration<MaterialActividad>
{
    public void Configure(EntityTypeBuilder<MaterialActividad> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("MaterialId");
    }
}
