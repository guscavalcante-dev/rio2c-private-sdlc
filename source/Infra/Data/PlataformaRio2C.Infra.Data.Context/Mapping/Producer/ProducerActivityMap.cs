//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    public class ProducerActivityMap : EntityTypeConfiguration<ProducerActivity>
//    {
//        public ProducerActivityMap()
//        {
//            this.Ignore(p => p.Uid);

//            this.HasKey(d => new { d.ProducerId, d.ActivityId });

//            //Relationships
//            this.HasRequired(t => t.Producer)
//                .WithMany(t => t.ProducerActivitys)
//                .HasForeignKey(d => d.ProducerId);

//            this.HasRequired(t => t.Activity)
//                .WithMany()
//                .HasForeignKey(d => d.ActivityId);

//            this.ToTable("ProducerActivity");
//        }
//    }
//}
