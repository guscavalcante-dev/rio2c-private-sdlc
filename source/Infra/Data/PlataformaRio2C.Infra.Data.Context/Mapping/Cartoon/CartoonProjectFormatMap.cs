// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2022
// ***********************************************************************
// <copyright file="CartoonProjectFormatMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CartoonProjectFormatMap</summary>
    public class CartoonProjectFormatMap : EntityTypeConfiguration<CartoonProjectFormat>
    {
        /// <summary>Initializes a new instance of the <see cref="CartoonProjectFormatMap"/> class.</summary>
        public CartoonProjectFormatMap()
        {
            this.ToTable("CartoonProjectFormats");

            // Relationships
        }
    }
}