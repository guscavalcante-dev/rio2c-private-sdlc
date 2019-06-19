using System.Data.Entity.ModelConfiguration;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            this.Property(p => p.Name)
                .HasMaxLength(City.NameLength);


            //Relationship
            this.HasRequired(t => t.State)
                .WithMany()
                .HasForeignKey(d => d.StateId);

            this.ToTable("City");
        }
    }
}
