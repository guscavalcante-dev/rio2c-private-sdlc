//using PlataformaRio2C.Domain.Entities;
//using System.Data.Entity.ModelConfiguration;

//namespace PlataformaRio2C.Infra.Data.Context.Mapping
//{
//    public class MailMap : EntityTypeConfiguration<Mail>
//    {
//        public MailMap()
//        {
//            Property(u => u.Message)
//                .HasColumnType("nvarchar(max)")
//                .HasMaxLength(int.MaxValue);

//            //Property(u => u.Subject)
//            //    .HasColumnType("nvarchar(max)")
//            //    .HasMaxLength(int.MaxValue);

//            ////Relationships
//            //this.HasRequired(t => t.Sender)
//            //   .WithMany()
//            //   .HasForeignKey(d => d.SenderId);

//            //this.HasRequired(t => t.Recipient)
//            // .WithMany()
//            // .HasForeignKey(d => d.RecipientId);

//            this.ToTable("Mail");
//        }
//    }
//}
