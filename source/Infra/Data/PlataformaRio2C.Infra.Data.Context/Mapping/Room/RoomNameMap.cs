using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class RoomNameMap : EntityTypeConfiguration<RoomName>
    {
        public RoomNameMap()
        {
            this.Ignore(p => p.LanguageCode);

            this.Property(p => p.Value)
                .HasMaxLength(RoomName.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Room)
                .WithMany(e => e.Names)
                .HasForeignKey(d => d.RoomId);

            this.HasRequired(t => t.Language)
              .WithMany()
              .HasForeignKey(d => d.LanguageId);

            this.ToTable("RoomName");
        }
    }
}
