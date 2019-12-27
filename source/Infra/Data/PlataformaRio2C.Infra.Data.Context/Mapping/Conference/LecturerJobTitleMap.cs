//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    public class LecturerJobTitleMap : EntityTypeConfiguration<LecturerJobTitle>
//    {
//        public LecturerJobTitleMap()
//        {
//            this.Ignore(p => p.LanguageCode);

//            this.Property(p => p.Value)
//                .HasMaxLength(CollaboratorJobTitle.ValueMaxLength);

//            //Relationships
//            this.HasRequired(t => t.Lecturer)
//                .WithMany(e => e.JobTitles)
//                .HasForeignKey(d => d.LecturerId);

//            this.HasRequired(t => t.Language)
//              .WithMany()
//              .HasForeignKey(d => d.LanguageId);

//            this.ToTable("LecturerJobTitle");
//        }
//    }
//}
