//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    public class ConferenceLecturerMap : EntityTypeConfiguration<ConferenceLecturer>
//    {
//        public ConferenceLecturerMap()
//        {           

//            //Relationships  
//            this.HasOptional(t => t.Collaborator)
//                .WithMany()
//                .HasForeignKey(d => d.CollaboratorId);

//            this.HasOptional(t => t.Lecturer)
//                .WithMany()
//                .HasForeignKey(d => d.LecturerId);

//            this.HasRequired(t => t.Conference)
//               .WithMany(t=> t.Lecturers)
//               .HasForeignKey(d => d.ConferenceId);

//            this.HasOptional(t => t.RoleLecturer)
//               .WithMany()
//               .HasForeignKey(d => d.RoleLecturerId);

//            this.ToTable("ConferenceLecturer");
//        }
//    }
//}
