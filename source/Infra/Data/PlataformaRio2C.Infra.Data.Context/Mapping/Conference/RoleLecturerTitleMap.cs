using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class RoleLecturerTitleMap : EntityTypeConfiguration<RoleLecturerTitle>
    {
        public RoleLecturerTitleMap()
        {
            this.Ignore(p => p.LanguageCode);

            this.Property(p => p.Value)
                .HasMaxLength(RoleLecturerTitle.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.RoleLecturer)
                .WithMany(e => e.Titles)
                .HasForeignKey(d => d.RoleLecturerId);

            this.HasRequired(t => t.Language)
              .WithMany()
              .HasForeignKey(d => d.LanguageId);

            this.ToTable("RoleLecturerTitle");
        }
    }
}
