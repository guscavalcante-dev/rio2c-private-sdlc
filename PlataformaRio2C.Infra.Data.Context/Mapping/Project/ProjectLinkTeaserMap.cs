using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProjectLinkTeaserMap : EntityTypeConfiguration<ProjectLinkTeaser>
    {
        public ProjectLinkTeaserMap()
        {
            Property(u => u.Value)
            .HasMaxLength(ProjectLinkTeaser.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.LinksTeaser)
                .HasForeignKey(d => d.ProjectId);

            this.ToTable("ProjectLinkTeaser");
        }
    }
}
