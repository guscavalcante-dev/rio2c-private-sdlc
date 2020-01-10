//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    public class PlayerActivityMap : EntityTypeConfiguration<PlayerActivity>
//    {
//        public PlayerActivityMap()
//        {
//            this.Ignore(p => p.Uid);

//            this.HasKey(d => new { d.PlayerId, d.ActivityId });

//            //Relationships
//            this.HasRequired(t => t.Player)
//                .WithMany()
//                .HasForeignKey(d => d.PlayerId);

//            this.HasRequired(t => t.Activity)
//                .WithMany()
//                .HasForeignKey(d => d.ActivityId);

//            this.ToTable("PlayerActivity");
//        }
//    }
//}
