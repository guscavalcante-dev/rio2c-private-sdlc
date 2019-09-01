// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="UserMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>UserMap</summary>
    public class UserMap : EntityTypeConfiguration<User>
    {
        /// <summary>Initializes a new instance of the <see cref="UserMap"/> class.</summary>
        public UserMap()
        {
            this.ToTable("Users");

            this.HasKey(u => u.Id);

            this.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(u => u.Email)
                .HasMaxLength(256);

            this.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(u => u.PasswordHash)
               .HasMaxLength(8000);

            this.Property(u => u.SecurityStamp)
               .HasMaxLength(8000);

            this.Property(u => u.PhoneNumber)
              .HasMaxLength(8000);

            // Relationships
            //this.HasOptional(t => t.Collaborator)
            //    .WithRequired(e => e.User);
            //.HasForeignKey(d => d.Id);

            this.HasMany(t => t.Roles)
                .WithMany()
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("RoleId");
                    cs.ToTable("UsersRoles");
                });

            // Ignores
            this.Ignore(u => u.CreateUserId);
            this.Ignore(u => u.UpdateUserId);
        }
    }
}