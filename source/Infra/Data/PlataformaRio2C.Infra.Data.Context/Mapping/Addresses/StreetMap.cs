// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 08-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="StreetMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Data.Entity.ModelConfiguration;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>StreetMap</summary>
    class StreetMap : EntityTypeConfiguration<Street>
    {
        /// <summary>Initializes a new instance of the <see cref="StreetMap"/> class.</summary>
        public StreetMap()
        {
            this.ToTable("Streets");

            this.Property(p => p.Name)
                .HasMaxLength(Street.NameMaxLength);

            this.Property(p => p.ZipCode)
                .HasMaxLength(Street.ZipCodeMaxLength);

            // Relationships
            this.HasRequired(t => t.Neighborhood)
                .WithMany()
                .HasForeignKey(d => d.NeighborhoodId);

            this.HasMany(t => t.Addresses)
                .WithRequired(e => e.Street)
                .HasForeignKey(e => e.StreetId);
        }
    }
}