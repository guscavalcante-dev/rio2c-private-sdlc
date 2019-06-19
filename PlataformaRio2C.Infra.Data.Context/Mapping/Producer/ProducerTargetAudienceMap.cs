using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProducerTargetAudienceMap : EntityTypeConfiguration<ProducerTargetAudience>
    {
        public ProducerTargetAudienceMap()
        {
            this.Ignore(p => p.Uid);

            this.HasKey(d => new { d.ProducerId, d.TargetAudienceId });

            //Relationships
            this.HasRequired(t => t.Producer)
                .WithMany(t => t.ProducerTargetAudience)
                .HasForeignKey(d => d.ProducerId);

            this.HasRequired(t => t.TargetAudience)
                .WithMany()
                .HasForeignKey(d => d.TargetAudienceId);


            this.ToTable("ProducerTargetAudience");
        }
    }
}
