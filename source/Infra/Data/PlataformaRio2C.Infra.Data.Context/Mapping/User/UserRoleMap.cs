// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-06-2019
// ***********************************************************************
// <copyright file="UserRoleMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>UserRoleMap</summary>
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        /// <summary>Initializes a new instance of the <see cref="UserRoleMap"/> class.</summary>
        public UserRoleMap()
        {
            this.ToTable("UsersRoles");

            this.HasKey(p => new {p.UserId, p.RoleId });

            this.Ignore(p => p.Id);
            this.Ignore(p => p.Uid);
            this.Ignore(p => p.CreateDate);

            this.HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);

            this.HasRequired(t => t.Role)
                .WithMany()
                .HasForeignKey(d => d.RoleId);
        }
    }
}
