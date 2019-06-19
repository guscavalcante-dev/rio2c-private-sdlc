using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class NegotiationConfigMap : EntityTypeConfiguration<NegotiationConfig>
    {
        public NegotiationConfigMap()
        {
            this.Property(t => t.Date)
                .IsRequired();

            this.Property(t => t.StartTime)
               .IsRequired();

            this.Property(t => t.EndTime)
             .IsRequired();

            this.ToTable("NegotiationConfig");
        }
    }
}
