// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="CartoonProjectLogLineMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CartoonProjectLogLineMap</summary>
    public class CartoonProjectLogLineMap : EntityTypeConfiguration<CartoonProjectLogLine>
    {
        /// <summary>Initializes a new instance of the <see cref="CartoonProjectLogLineMap"/> class.</summary>
        public CartoonProjectLogLineMap()
        {
            this.ToTable("CartoonProjectLogLines");

            //this.Ignore(p => p.LanguageCode);

            Property(u => u.Value)
                .HasMaxLength(8000);

            //Relationships
            this.HasRequired(t => t.CartoonProject)
                .WithMany(e => e.CartoonProjectLogLines)
                .HasForeignKey(t => t.CartoonProjectId);
        }
    }
}