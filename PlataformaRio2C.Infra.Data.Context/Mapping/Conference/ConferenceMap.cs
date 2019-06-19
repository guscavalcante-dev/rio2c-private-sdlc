using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class ConferenceMap : EntityTypeConfiguration<Conference>
    {
        public ConferenceMap()
        {
         
            Property(u => u.Info)
               .HasMaxLength(Conference.InfoMaxLength);

            this.Property(p => p.StartTime)             
             .IsRequired();

            this.Property(p => p.EndTime)            
            .IsRequired();

            //Relationships 
            this.HasOptional(t => t.Room)
              .WithMany()
              .HasForeignKey(d => d.RoomId);

            this.ToTable("Conference");
        }
    }
}
