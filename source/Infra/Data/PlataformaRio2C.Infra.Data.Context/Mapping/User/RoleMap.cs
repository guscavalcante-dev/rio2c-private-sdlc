using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.Ignore(p => p.Uid);
            this.Ignore(p => p.CreationDate);           

            this.ToTable("AspNetRoles");
        }
    }
}
