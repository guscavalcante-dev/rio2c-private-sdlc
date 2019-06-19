using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ConferenceTitleMap : EntityTypeConfiguration<ConferenceTitle>
    {
        public ConferenceTitleMap()
        {
            this.Ignore(p => p.LanguageCode);

            this.Property(p => p.Value)
                .HasMaxLength(ConferenceTitle.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Conference)
                .WithMany(t => t.Titles)
                .HasForeignKey(d => d.ConferenceId);

            this.ToTable("ConferenceTitle");
        }
    }
}
