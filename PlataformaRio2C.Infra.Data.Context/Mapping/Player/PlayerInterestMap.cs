using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class PlayerInterestMap : EntityTypeConfiguration<PlayerInterest>
    {
        public PlayerInterestMap()
        {
            this.Ignore(p => p.Uid);
            this.Ignore(p => p.Id);

            this.Ignore(p => p.PlayerUid);         
            this.Ignore(p => p.InterestUid);

            this.HasKey(p => new { p.PlayerId, p.InterestId });

            //Relationships
            this.HasRequired(t => t.Player)
                .WithMany(p => p.Interests)
                .HasForeignKey(d => d.PlayerId);

            this.HasRequired(t => t.Interest)
              .WithMany()
              .HasForeignKey(d => d.InterestId);

            this.ToTable("PlayerInterest");
        }
    }
}
