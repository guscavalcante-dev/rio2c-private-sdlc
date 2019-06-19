using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            Property(u => u.AddressValue)
              .HasMaxLength(Address.AddressValueMaxLength);

            //Property(u => u.CountryId).IsRequired();
            //Property(u => u.StateId).IsRequired();
            //Property(u => u.CityId).IsRequired();

            //relationships
            this.HasMany<Country>(t => t.Countries)
               //.WithRequired( s=> s.Address)
               ;

            this.HasMany<State>(t => t.States)
               //.WithRequired(s => s.Address)
               ;

            this.HasMany<City>(t => t.Cities)
               //.WithRequired(s => s.Address)
               ;

            this.ToTable("Address");
        }
    }
}
