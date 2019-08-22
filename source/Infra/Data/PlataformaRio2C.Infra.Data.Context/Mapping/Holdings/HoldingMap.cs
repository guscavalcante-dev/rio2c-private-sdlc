// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-22-2019
// ***********************************************************************
// <copyright file="HoldingMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>HoldingMap</summary>
    public class HoldingMap : EntityTypeConfiguration<Holding>
    {
        /// <summary>Initializes a new instance of the <see cref="HoldingMap"/> class.</summary>
        public HoldingMap()
        {
            this.ToTable("Holdings");

            this.Property(t => t.Name)
                .HasMaxLength(Holding.NameMaxLength)
                .IsRequired();

            // Relationships
            this.HasRequired(t => t.Updater)
                .WithMany(e => e.UpdatedHoldings)
                .HasForeignKey(d => d.UpdateUserId);

            this.HasMany(t => t.Organizations)
                .WithOptional(e => e.Holding)
                .HasForeignKey(e => e.HoldingId);
        }
    }
}