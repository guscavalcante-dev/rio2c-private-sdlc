using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProjectSummaryMap : EntityTypeConfiguration<ProjectSummary>
    {
        public ProjectSummaryMap()
        {
            this.Ignore(p => p.LanguageCode);

            Property(u => u.Value)
                .HasColumnType("nvarchar(max)")
                .HasMaxLength(int.MaxValue);

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.Summaries)
                .HasForeignKey(d => d.ProjectId);

            this.ToTable("ProjectSummary");
        }
    }
}
