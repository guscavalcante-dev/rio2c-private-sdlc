using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class HoldingDescriptionMap : EntityTypeConfiguration<HoldingDescription>
    {
        public HoldingDescriptionMap()
        {
            this.Ignore(p => p.LanguageCode);

            this.Property(p => p.Value)
                .HasMaxLength(HoldingDescription.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Holding)
                .WithMany(e => e.Descriptions)
                .HasForeignKey(d => d.HoldingId);

            this.HasRequired(t => t.Language)
               .WithMany()
               .HasForeignKey(d => d.LanguageId);


            this.ToTable("HoldingDescription");
        }
    }
}
