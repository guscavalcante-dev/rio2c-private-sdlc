using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class CollaboratorJobTitleMap : EntityTypeConfiguration<CollaboratorJobTitle>
    {
        public CollaboratorJobTitleMap()
        {
            this.Ignore(p => p.LanguageCode);

            this.Property(p => p.Value)
                .HasMaxLength(CollaboratorJobTitle.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Collaborator)
                .WithMany(e => e.JobTitles)
                .HasForeignKey(d => d.CollaboratoId);

            this.HasRequired(t => t.Language)
              .WithMany()
              .HasForeignKey(d => d.LanguageId);

            this.ToTable("CollaboratorJobTitle");
        }
    }
}
