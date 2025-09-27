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
    internal sealed class ActividadPonenteConfiguration : IEntityTypeConfiguration<ActividadPonente>
    {
        public void Configure(EntityTypeBuilder<ActividadPonente> builder)
        {
            builder.HasKey(ap => ap.Id);

            builder.Property(ap => ap.Id)
                .HasColumnName("idActividadPonente")
                .IsRequired();
        }
    }
}
