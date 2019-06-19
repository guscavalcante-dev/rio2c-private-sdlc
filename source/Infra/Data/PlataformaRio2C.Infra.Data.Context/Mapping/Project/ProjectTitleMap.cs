using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProjectTitleMap : EntityTypeConfiguration<ProjectTitle>
    {
        public ProjectTitleMap()
        {
            this.Ignore(p => p.LanguageCode);

            Property(u => u.Value)
               .HasMaxLength(ProjectTitle.ValueMaxLength);            

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.Titles)
                .HasForeignKey(d => d.ProjectId);

            this.ToTable("ProjectTitle");
        }
    }
}
