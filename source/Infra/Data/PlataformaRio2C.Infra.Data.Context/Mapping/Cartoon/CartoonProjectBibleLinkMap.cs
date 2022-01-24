// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="CartoonProjectBibleLinkMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CartoonProjectBibleLinkMap</summary>
    public class CartoonProjectBibleLinkMap : EntityTypeConfiguration<CartoonProjectBibleLink>
    {
        /// <summary>Initializes a new instance of the <see cref="CartoonProjectBibleLinkMap"/> class.</summary>
        public CartoonProjectBibleLinkMap()
        {
            this.ToTable("CartoonProjectBibleLinks");

            Property(u => u.Value)
                .HasMaxLength(CartoonProjectBibleLink.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.CartoonProject)
                .WithMany(e => e.CartoonProjectBibleLinks)
                .HasForeignKey(t => t.CartoonProjectId);
        }
    }
}