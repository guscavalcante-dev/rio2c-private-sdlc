using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    class StateMap : EntityTypeConfiguration<State>
    {
        public StateMap()
        {
            this.Property(p => p.StateName)
                .HasMaxLength(State.NameLength);

            this.Property(p => p.StateCode)
                .HasMaxLength(State.CodeLength);

            //Relationship
            this.HasRequired(t => t.Country)
                .WithMany()
                .HasForeignKey(d => d.CountryId);

            this.ToTable("State");
        }
    }
}
