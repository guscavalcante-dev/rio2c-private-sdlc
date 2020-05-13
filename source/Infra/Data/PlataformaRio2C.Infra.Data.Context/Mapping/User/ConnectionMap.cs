// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 05-13-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-13-2020
// ***********************************************************************
// <copyright file="ConnectionMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConnectionMap</summary>
    public class ConnectionMap : EntityTypeConfiguration<Connection>
    {
        /// <summary>Initializes a new instance of the <see cref="ConnectionMap"/> class.</summary>
        public ConnectionMap()
        {
            this.ToTable("Connections");

            this.HasKey(m => m.ConnectionId);

            this.Property(m => m.UserAgent)
                .HasMaxLength(Connection.UserAgentMaxLength);

            // Relationships
            this.HasRequired(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId);

            // Ignores
            this.Ignore(m => m.Id);
            this.Ignore(m => m.IsDeleted);
            this.Ignore(m => m.CreateUserId);
            this.Ignore(m => m.UpdateUserId);
        }
    }
}