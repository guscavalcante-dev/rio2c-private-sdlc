using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class UserUseTermMap : EntityTypeConfiguration<UserUseTerm>
    {
        public UserUseTermMap()
        {
            //Relationships          
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserUseTerms)
                .HasForeignKey(d => d.UserId);

            this.HasRequired(t => t.Event)
                .WithMany()
                .HasForeignKey(d => d.EventId);

            this.HasRequired(t => t.Role)
                .WithMany()
                .HasForeignKey(d => d.RoleId);

            this.ToTable("UserUseTerm");
        }
    }
}
