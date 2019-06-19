using PlataformaRio2C.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(u => u.Id);

            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(150);

            Property(u => u.Email)                
                .HasMaxLength(256);

            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256);

            Property(u => u.PasswordHash)               
               .HasMaxLength(8000);

            Property(u => u.SecurityStamp)
               .HasMaxLength(8000);

            Property(u => u.PhoneNumber)
              .HasMaxLength(8000);

            this.HasMany(t => t.Roles)                
                .WithMany()
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("RoleId");
                    cs.ToTable("AspNetUserRoles");
                });           


            ToTable("AspNetUsers");
        }
    }
}
