using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ConferenceSynopsisMap : EntityTypeConfiguration<ConferenceSynopsis>
    {
        public ConferenceSynopsisMap()
        {

            this.Ignore(p => p.LanguageCode);

            this.Property(p => p.Value)
                .HasMaxLength(ConferenceSynopsis.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Conference)
                .WithMany(t => t.Synopses)
                .HasForeignKey(d => d.ConferenceId);

            this.ToTable("ConferenceSynopsis");
        }
    }
}
