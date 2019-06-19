using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class PlayerMap : EntityTypeConfiguration<Player>
    {
        public PlayerMap()
        {
            this.Ignore(p => p.HoldingUid);

            this.Property(t => t.Name)
                .HasMaxLength(Player.NameMaxLength)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                        new IndexAnnotation(
                                                                new IndexAttribute("IX_Name", 2)
                                                                {
                                                                    IsUnique = true
                                                                }
                                                            )
                                                    )
                .IsRequired();

            Property(u => u.CompanyName)
              .HasMaxLength(Player.CompanyNameMaxLength);

            Property(u => u.Website)
               .HasMaxLength(Player.WebSiteMaxLength);

            Property(u => u.SocialMedia)
               .HasMaxLength(Player.SocialMediaMaxLength);

            Property(u => u.TradeName)
               .HasMaxLength(Player.TradeNameMaxLength);

            //Relationships
            this.HasRequired(t => t.Holding)
               .WithMany()
               .HasForeignKey(d => d.HoldingId);

            this.HasOptional(t => t.Image)
                .WithMany()
                .HasForeignKey(d => d.ImageId);

            this.HasOptional(t => t.Address)
              .WithMany()
              .HasForeignKey(d => d.AddressId);

            this.ToTable("Player");
        }
    }
}
