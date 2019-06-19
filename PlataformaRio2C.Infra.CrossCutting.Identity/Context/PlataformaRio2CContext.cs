using Microsoft.AspNet.Identity.EntityFramework;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using System.Data.Entity;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Context
{
    public class PlataformaRio2CContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        static PlataformaRio2CContext()
        {
            Database.SetInitializer<PlataformaRio2CContext>(null);
        }

        public PlataformaRio2CContext()
            : base("PlataformaRio2CConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public static PlataformaRio2CContext Create()
        {
            return new PlataformaRio2CContext();
        }

        object placeHolderVariable;
    }
}
