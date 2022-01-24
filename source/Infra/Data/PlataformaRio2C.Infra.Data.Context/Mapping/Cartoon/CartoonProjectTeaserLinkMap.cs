// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="CartoonProjectTeaserLinkMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CartoonProjectTeaserLinkMap</summary>
    public class CartoonProjectTeaserLinkMap : EntityTypeConfiguration<CartoonProjectTeaserLink>
    {
        /// <summary>Initializes a new instance of the <see cref="CartoonProjectTeaserLinkMap"/> class.</summary>
        public CartoonProjectTeaserLinkMap()
        {
            this.ToTable("CartoonProjectTeaserLinks");

            Property(u => u.Value)
                .HasMaxLength(CartoonProjectTeaserLink.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.CartoonProject)
                .WithMany(e => e.CartoonProjectTeaserLinks)
                .HasForeignKey(t => t.CartoonProjectId);
        }
    }
}