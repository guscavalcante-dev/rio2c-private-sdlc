using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            Property(u => u.Pitching)
                .IsRequired();

            //Relationships
            this.HasMany(c => c.Titles)
                  .WithRequired(t => t.Project);

            this.HasRequired(t => t.Producer)
             .WithMany(e => e.Projects)
             .HasForeignKey(d => d.ProducerId);           

            this.ToTable("Project");
        }
    }
}
