// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-12-2019
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
            //this.Property(t => t.Date)
            //    .IsRequired();

            this.ToTable("SalesPlatform");
        }
    }
}