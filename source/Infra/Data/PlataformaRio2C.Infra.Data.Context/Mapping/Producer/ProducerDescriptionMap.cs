//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    public class ProducerDescriptionMap : EntityTypeConfiguration<ProducerDescription>
//    {
//        public ProducerDescriptionMap()
//        {
//            this.Ignore(p => p.LanguageCode);

//            this.Property(p => p.Value)
//                .HasMaxLength(PlayerDescription.ValueMaxLength);

//            //Relationships
//            this.HasRequired(t => t.Producer)
//                .WithMany(e => e.Descriptions)
//                .HasForeignKey(d => d.ProducerId);


//            this.ToTable("ProducerDescription");
//        }
//    }
//}
