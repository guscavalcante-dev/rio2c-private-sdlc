using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProjectLogLineMap : EntityTypeConfiguration<ProjectLogLine>
    {
        public ProjectLogLineMap()
        {
            this.Ignore(p => p.LanguageCode);

            Property(u => u.Value)
             .HasMaxLength(8000);

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.LogLines)
                .HasForeignKey(d => d.ProjectId);

            this.ToTable("ProjectLogLine");
        }
    }
}
