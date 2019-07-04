// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Identity
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-04-2019
// ***********************************************************************
// <copyright file="Configuration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Migrations
{
    /// <summary>Configuration</summary>
    internal sealed class Configuration : DbMigrationsConfiguration<PlataformaRio2C.Infra.CrossCutting.Identity.Context.PlataformaRio2CContext>
    {
        /// <summary>Initializes a new instance of the <see cref="Configuration"/> class.</summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>Runs after upgrading to the latest migration to allow seed data to be updated.</summary>
        /// <param name="context">Context to be used for updating seed data.</param>
        /// <remarks>
        /// Note that the database may already contain seed data when this method runs. This means that
        /// implementations of this method must check whether or not seed data is present and/or up-to-date
        /// and then only make changes if necessary and in a non-destructive way. The
        /// <see cref="M:System.Data.Entity.Migrations.DbSetMigrationsExtensions.AddOrUpdate``1(System.Data.Entity.IDbSet{``0},``0[])"/>
        /// can be used to help with this, but for seeding large amounts of data it may be necessary to do less
        /// granular checks if performance is an issue.
        /// If the <see cref="T:System.Data.Entity.MigrateDatabaseToLatestVersion`2"/> database
        /// initializer is being used, then this method will be called each time that the initializer runs.
        /// If one of the <see cref="T:System.Data.Entity.DropCreateDatabaseAlways`1"/>, <see cref="T:System.Data.Entity.DropCreateDatabaseIfModelChanges`1"/>,
        /// or <see cref="T:System.Data.Entity.CreateDatabaseIfNotExists`1"/> initializers is being used, then this method will not be
        /// called and the Seed method defined in the initializer should be used instead.
        /// </remarks>
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
