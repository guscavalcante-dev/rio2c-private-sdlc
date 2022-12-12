// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-30-2022
// ***********************************************************************
// <copyright file="SalesPlatformMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>SalesPlatformMap</summary>
    public class SalesPlatformMap : EntityTypeConfiguration<SalesPlatform>
    {
        /// <summary>Initializes a new instance of the <see cref="SalesPlatformMap"/> class.</summary>
        public SalesPlatformMap()
        {
            this.ToTable("SalesPlatforms");

            this.Property(t => t.Name)
                .HasMaxLength(SalesPlatform.NameMaxLenght);

            this.Property(t => t.ApiKey)
                .HasMaxLength(SalesPlatform.ApiKeyMaxLength);

            this.Property(t => t.ApiSecret)
                .HasMaxLength(SalesPlatform.ApiSecretMaxLength);

            // Relationships
            this.HasMany(t => t.AttendeeSalesPlatforms)
                .WithRequired(e => e.SalesPlatform)
                .HasForeignKey(e => e.SalesPlatformId);
        }
    }
}