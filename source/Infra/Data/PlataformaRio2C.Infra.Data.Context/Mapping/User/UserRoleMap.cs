using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            this.HasKey(p => new {p.UserId, p.RoleId });

            this.Ignore(p => p.Id);
            this.Ignore(p => p.Uid);
            this.Ignore(p => p.CreationDate);

            this.HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);

            this.HasRequired(t => t.Role)
                .WithMany()
                .HasForeignKey(d => d.RoleId);

            this.ToTable("AspNetUserRoles");
        }
    }
}
