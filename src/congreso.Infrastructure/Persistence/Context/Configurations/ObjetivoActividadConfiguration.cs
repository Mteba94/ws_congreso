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
    internal sealed class ObjetivoActividadConfiguration : IEntityTypeConfiguration<ObjetivoActividad>
    {
        public void Configure(EntityTypeBuilder<ObjetivoActividad> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("idObjetivo");
        }
    }
}
