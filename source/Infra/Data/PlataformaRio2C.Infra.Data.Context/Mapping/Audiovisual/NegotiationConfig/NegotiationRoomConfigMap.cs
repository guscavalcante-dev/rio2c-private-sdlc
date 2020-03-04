//using PlataformaRio2C.Domain.Entities;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Infrastructure.Annotations;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    public class NegotiationRoomConfigMap : EntityTypeConfiguration<NegotiationRoomConfig>
//    {
//        public NegotiationRoomConfigMap()
//        {
//            //Relationships
//            this.HasRequired(t => t.Room)
//                .WithMany()
//                .HasForeignKey(d => d.RoomId);

//            this.HasRequired(t => t.NegotiationConfig)
//               .WithMany(e => e.Rooms)
//               .HasForeignKey(d => d.NegotiationConfigId);

//            this.ToTable("NegotiationRoomConfig");
//        }
//    }
//}
