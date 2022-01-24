// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="CartoonProjectTitleMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CartoonProjectTitleMap</summary>
    public class CartoonProjectTitleMap : EntityTypeConfiguration<CartoonProjectTitle>
    {
        /// <summary>Initializes a new instance of the <see cref="CartoonProjectTitleMap"/> class.</summary>
        public CartoonProjectTitleMap()
        {
            this.ToTable("CartoonProjectTitles");

            //this.Ignore(p => p.LanguageCode);

            Property(u => u.Value)
               .HasMaxLength(CartoonProjectTitle.ValueMaxLength);            

            //Relationships
            this.HasRequired(t => t.CartoonProject)
                .WithMany(e => e.CartoonProjectTitles)
                .HasForeignKey(t => t.CartoonProjectId);
        }
    }
}