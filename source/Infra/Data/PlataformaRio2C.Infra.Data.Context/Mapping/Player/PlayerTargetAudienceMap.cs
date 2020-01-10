//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    public class PlayerTargetAudienceMap : EntityTypeConfiguration<PlayerTargetAudience>
//    {
//        public PlayerTargetAudienceMap()
//        {
//            this.Ignore(p => p.Uid);

//            this.HasKey(d => new { d.PlayerId, d.TargetAudienceId });

//            //Relationships
//            this.HasRequired(t => t.Player)
//                .WithMany(t => t.PlayerTargetAudience)
//                .HasForeignKey(d => d.PlayerId);

//            this.HasRequired(t => t.TargetAudience)
//                .WithMany()
//                .HasForeignKey(d => d.TargetAudienceId);


//            this.ToTable("PlayerTargetAudience");
//        }
//    }
//}
