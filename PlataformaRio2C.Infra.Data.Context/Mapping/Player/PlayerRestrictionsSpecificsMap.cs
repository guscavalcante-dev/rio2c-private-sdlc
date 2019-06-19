using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class PlayerRestrictionsSpecificsMap : EntityTypeConfiguration<PlayerRestrictionsSpecifics>
    {
        public PlayerRestrictionsSpecificsMap()
        {
            this.Ignore(p => p.LanguageCode);

            this.Property(p => p.Value)
                .HasMaxLength(PlayerDescription.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Player)
                .WithMany(e => e.RestrictionsSpecifics)
                .HasForeignKey(d => d.PlayerId);


            this.ToTable("PlayerRestrictionsSpecifics");
        }
    }
}
