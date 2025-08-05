using congreso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace congreso.Infrastructure.Persistence.Context.Configurations
{
    internal sealed class RoleUsuarioConfiguration : IEntityTypeConfiguration<RoleUsuario>
    {
        public void Configure(EntityTypeBuilder<RoleUsuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idUsuarioRoles");
        }
    }
}
