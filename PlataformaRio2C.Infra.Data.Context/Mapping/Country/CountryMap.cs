using System.Data.Entity.ModelConfiguration;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            this.Property(p => p.CountryName)
                .HasMaxLength(Country.NameLength);

            this.Property(p => p.CountryCode)
                .HasMaxLength(Country.CodeLength);

            this.ToTable("Country");
        }
    }
}
