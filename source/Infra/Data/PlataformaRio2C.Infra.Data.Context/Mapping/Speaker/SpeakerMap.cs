//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    class SpeakerMap : EntityTypeConfiguration<Speaker>
//    {
//        public SpeakerMap()
//        {
//            this.HasKey(d => new { d.CollaboratorId, d.Id });

//            //Relationships
//            //this.HasRequired(t => t.Event)
//            //   .WithMany()
//            //   .HasForeignKey(d => d.EventId);

//            this.HasRequired(t => t.Collaborator)
//               .WithMany()
//               .HasForeignKey(d => d.CollaboratorId);

//            this.ToTable("Speaker");

//        }
//    }
//}
