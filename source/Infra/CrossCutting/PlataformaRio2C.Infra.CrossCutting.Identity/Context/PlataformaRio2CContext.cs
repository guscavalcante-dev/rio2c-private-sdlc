// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Identity
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="PlataformaRio2CContext.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNet.Identity.EntityFramework;
using PlataformaRio2C.Infra.CrossCutting.Identity.Models;
using System.Data.Entity;

namespace PlataformaRio2C.Infra.CrossCutting.Identity.Context
{
    /// <summary></summary>
    public class PlataformaRio2CContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        /// <summary>Initializes the <see cref="PlataformaRio2CContext"/> class.</summary>
        static PlataformaRio2CContext()
        {
            Database.SetInitializer<PlataformaRio2CContext>(null);
        }

        /// <summary>Initializes a new instance of the <see cref="PlataformaRio2CContext"/> class.</summary>
        public PlataformaRio2CContext()
            : base("PlataformaRio2CConnection")
        {
        }

        /// <summary>Maps table names, and sets up relationships between the various user entities</summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<CustomRole>().ToTable("Roles");
            modelBuilder.Entity<CustomUserRole>().ToTable("UsersRoles");
        }

        /// <summary>Saves all changes made in this context to the underlying database.</summary>
        /// <returns>
        /// The number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        /// <summary>Creates this instance.</summary>
        /// <returns></returns>
        public static PlataformaRio2CContext Create()
        {
            return new PlataformaRio2CContext();
        }

        object placeHolderVariable;
    }
}
