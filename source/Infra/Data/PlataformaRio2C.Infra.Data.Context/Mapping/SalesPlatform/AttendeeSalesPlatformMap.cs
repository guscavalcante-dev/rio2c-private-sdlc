// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="AttendeeSalesPlatformMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeSalesPlatformMap</summary>
    public class AttendeeSalesPlatformMap : EntityTypeConfiguration<AttendeeSalesPlatform>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatformMap"/> class.</summary>
        public AttendeeSalesPlatformMap()
        {
            this.ToTable("AttendeeSalesPlatforms");

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany(e => e.AttendeeSalesPlatforms)
                .HasForeignKey(d => d.EditionId);

            this.HasRequired(t => t.SalesPlatform)
                .WithMany(e => e.AttendeeSalesPlatforms)
                .HasForeignKey(d => d.SalesPlatformId);

            this.HasMany(t => t.AttendeeSalesPlatformTicketTypes)
                .WithRequired(e => e.AttendeeSalesPlatform)
                .HasForeignKey(e => e.AttendeeSalesPlatformId);
        }
    }
}