//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    public class MailCollaboratorMap : EntityTypeConfiguration<MailCollaborator>
//    {
//        public MailCollaboratorMap()
//        {
//            this.Property(t => t.CreationDate)
//                .IsRequired();

//            this.HasKey(d => new { d.IdMail, d.IdCollaborator});


//            this.HasRequired(t => t.Mail)
//              .WithMany()
//              .HasForeignKey(d => d.IdMail);

//            this.HasRequired(t => t.Collaborator)
//              .WithMany()
//              .HasForeignKey(d => d.IdCollaborator);

//            this.ToTable("MailCollaborator");
//        }
//    }
//}
