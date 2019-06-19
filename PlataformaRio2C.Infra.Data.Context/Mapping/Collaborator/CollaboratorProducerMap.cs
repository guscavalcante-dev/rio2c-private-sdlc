using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class CollaboratorProducerMap : EntityTypeConfiguration<CollaboratorProducer>
    {
        public CollaboratorProducerMap()
        {
            this.Ignore(p => p.Uid);

            this.HasKey(d => new { d.CollaboratorId, d.ProducerId, d.EventId });

            //Relationships
            this.HasRequired(t => t.Collaborator)
                .WithMany(t => t.ProducersEvents)
                .HasForeignKey(d => d.CollaboratorId);

            this.HasRequired(t => t.Producer)
                .WithMany(t => t.EventsCollaborators)
                .HasForeignKey(d => d.ProducerId);

            this.HasRequired(t => t.Event)
               .WithMany()
               .HasForeignKey(d => d.EventId);

            this.ToTable("CollaboratorProducer");
        }
    }
}
