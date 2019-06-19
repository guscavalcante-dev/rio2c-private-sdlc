using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProducerEventMap : EntityTypeConfiguration<ProducerEvent>
    {
        public ProducerEventMap()
        {
            this.Ignore(p => p.Uid);

            this.HasKey(d => new { d.ProducerId, d.EventId });

            //Relationships
            this.HasRequired(t => t.Producer)
              .WithMany()
              .HasForeignKey(d => d.ProducerId);

            this.HasRequired(t => t.Event)
               .WithMany()
               .HasForeignKey(d => d.EventId);

            this.ToTable("ProducerEvent");
        }
    }
}
