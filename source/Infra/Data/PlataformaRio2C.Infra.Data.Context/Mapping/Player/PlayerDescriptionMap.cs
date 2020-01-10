//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    public class PlayerDescriptionMap : EntityTypeConfiguration<PlayerDescription>
//    {
//        public PlayerDescriptionMap()
//        {
//            this.Ignore(p => p.LanguageCode);

//            this.Property(p => p.Value)
//                .HasMaxLength(PlayerDescription.ValueMaxLength);

//            //Relationships
//            this.HasRequired(t => t.Player)
//                .WithMany(e => e.Descriptions)
//                .HasForeignKey(d => d.PlayerId);


//            this.ToTable("PlayerDescription");
//        }
//    }
//}
