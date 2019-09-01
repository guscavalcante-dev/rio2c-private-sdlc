// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="RoleMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>RoleMap</summary>
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        /// <summary>Initializes a new instance of the <see cref="RoleMap"/> class.</summary>
        public RoleMap()
        {
            this.ToTable("Roles");

            this.Ignore(p => p.Uid);
            this.Ignore(p => p.IsDeleted);
            this.Ignore(p => p.CreateDate);
            this.Ignore(p => p.CreateUserId);
            this.Ignore(p => p.UpdateDate);
            this.Ignore(p => p.UpdateUserId);
        }
    }
}