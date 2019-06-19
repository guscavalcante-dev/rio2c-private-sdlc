using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProjectLinkImageMap : EntityTypeConfiguration<ProjectLinkImage>
    {
        public ProjectLinkImageMap()
        {
            Property(u => u.Value)
                .HasMaxLength(ProjectLinkImage.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.LinksImage)
                .HasForeignKey(d => d.ProjectId);

            this.ToTable("ProjectLinkImage");
        }
    }
}
