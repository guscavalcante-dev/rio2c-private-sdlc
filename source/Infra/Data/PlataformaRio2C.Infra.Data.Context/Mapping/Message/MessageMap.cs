using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            Property(u => u.Text)
                .HasColumnType("nvarchar(max)")
                .HasMaxLength(int.MaxValue);

            Property(u => u.IsRead)
                .IsRequired();

            //Relationships
            this.HasRequired(t => t.Sender)
               .WithMany()
               .HasForeignKey(d => d.SenderId);

            this.HasRequired(t => t.Recipient)
             .WithMany()
             .HasForeignKey(d => d.RecipientId);

            this.ToTable("Message");
        }
    }
}
