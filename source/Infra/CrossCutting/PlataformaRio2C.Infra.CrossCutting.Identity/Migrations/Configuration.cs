namespace PlataformaRio2C.Infra.CrossCutting.Identity.Migrations
{
    using Tools.Extensions;
    using Domain.Enums;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<PlataformaRio2C.Infra.CrossCutting.Identity.Context.PlataformaRio2CContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PlataformaRio2C.Infra.CrossCutting.Identity.Context.PlataformaRio2CContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            var roles = default(UserRoles).ToEnumStrings(false).Where(e => e.Id != 0);
            foreach (var role in roles)
            {
                if (!context.Roles.Any(e => e.Name == role.Value))
                {
                    context.Roles.Add(new CustomRole(role.Value));
                }
            }

            //if (!context.Users.Any(u => u.UserName == "ldomingues@marlin.com.br"))
            //{
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);
            //    var user = new ApplicationUser { UserName = "ldomingues@marlin.com.br", Email = "ldomingues@marlin.com.br" };
            //    manager.Create(user, "teste@123");
            //    manager.AddToRole(user.Id, "Administrator");

            //    //UserRoles_Administrator
            //}


            if (!context.Users.Any(u => u.UserName == "projeto.rio2c@marlin.com.br"))
            {
                var store = new CustomUserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser, int>(store);
                var user = new ApplicationUser
                {
                    UserName = "projeto.rio2c@marlin.com.br",
                    Email = "projeto.rio2c@marlin.com.br",
                    CreationDate = DateTime.Now,
                    Uid = Guid.NewGuid(),
                    Active = true,
                    Name = "Master"
                };
                manager.Create(user, "r!o2c@2017");
                manager.AddToRole(user.Id, "Administrator");
            }

            //if (!context.Users.Any(u => u.UserName == "ldomingues@marlin.com.br"))
            //{
            //    var store = new CustomUserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser, int>(store);
            //    var user = new ApplicationUser
            //    {
            //        UserName = "ldomingues@marlin.com.br",
            //        Email = "ldomingues@marlin.com.br",
            //        CreationDate = DateTime.Now,
            //        Uid = Guid.NewGuid(),
            //        Active = true,
            //        Name = "Lucas Domingues"
            //    };
            //    manager.Create(user, "teste@123");
            //    manager.AddToRole(user.Id, "Player");
            //}


            //if (!context.Users.Any(u => u.UserName == "angelica@rio2c.com"))
            //{
            //    var store = new CustomUserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser, int>(store);
            //    var user = new ApplicationUser
            //    {
            //        UserName = "angelica@rio2c.com",
            //        Email = "angelica@rio2c.com",
            //        CreationDate = DateTime.Now,
            //        Uid = Guid.NewGuid(),
            //        Active = true,
            //        Name = "Angelica Cerdeira",
            //        Role = UserRoles.Administrator,

            //    };
            //    manager.Create(user, "r!o2c@2017");
            //    manager.AddToRole(user.Id, "Administrator");
            //}


            if (!context.Users.Any(u => u.UserName == "juliana@rio2c.com"))
            {
                var store = new CustomUserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser, int>(store);
                var user = new ApplicationUser
                {
                    UserName = "juliana@rio2c.com",
                    Email = "juliana@rio2c.com",
                    CreationDate = DateTime.Now,
                    Uid = Guid.NewGuid(),
                    Active = true,
                    Name = "Juliana Eberienos"                   

                };
                manager.Create(user, "280713Jg");
                manager.AddToRole(user.Id, "Administrator");
            }

            //if (!context.Users.Any(u => u.UserName == "ldomingues@marlin.com.br"))
            //{
            //    var store = new CustomUserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser, int>(store);
            //    var user = new ApplicationUser
            //    {
            //        UserName = "ldomingues@marlin.com.br",
            //        Email = "ldomingues@marlin.com.br",
            //        CreationDate = DateTime.Now,
            //        Uid = Guid.NewGuid(),
            //        Active = true,
            //        Name = "Lucas Domingues"

            //    };
            //    manager.Create(user, "teste@123");
            //    manager.AddToRole(user.Id, "Player");
            //}
        }
    }
}
