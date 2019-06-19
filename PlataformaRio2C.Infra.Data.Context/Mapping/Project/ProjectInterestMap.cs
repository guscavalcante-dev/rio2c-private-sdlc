using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ProjectInterestMap : EntityTypeConfiguration<ProjectInterest>
    {
        public ProjectInterestMap()
        {
            this.Ignore(p => p.Uid);
            this.Ignore(p => p.Id);

            this.Ignore(p => p.ProjectUid);
            this.Ignore(p => p.InterestUid);

            this.HasKey(p => new { p.ProjectId, p.InterestId });

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(p => p.Interests)
                .HasForeignKey(d => d.ProjectId);

            this.HasRequired(t => t.Interest)
              .WithMany()
              .HasForeignKey(d => d.InterestId);

            this.ToTable("ProjectInterest");
        }
    }
}
