using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class MusicalCommissionMap : EntityTypeConfiguration<MusicalCommission>
    {
        public MusicalCommissionMap()
        {
            this.HasKey(d => new { d.CollaboratorId, d.Id });

            this.HasRequired(t => t.Collaborator)
               .WithMany()
               .HasForeignKey(d => d.CollaboratorId);

            this.ToTable("MusicalCommission");
        }
    }
}
