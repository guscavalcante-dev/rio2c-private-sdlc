// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-12-2019
// ***********************************************************************
// <copyright file="CountryMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Data.Entity.ModelConfiguration;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CountryMap</summary>
    class CountryMap : EntityTypeConfiguration<Country>
    {
        /// <summary>Initializes a new instance of the <see cref="CountryMap"/> class.</summary>
        public CountryMap()
        {
            this.ToTable("Countries");

            this.Property(p => p.Name)
                .HasMaxLength(Country.NameMaxLength);

            this.Property(p => p.Code)
                .HasMaxLength(Country.CodeMaxLength);

            // Relationships
            this.HasMany(t => t.States)
                .WithRequired(e => e.Country)
                .HasForeignKey(e => e.CountryId);

            this.HasMany(t => t.Addresses)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.CountryId);
        }
    }
}