// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="CartoonProjectMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CartoonProjectMap</summary>
    public class CartoonProjectMap : EntityTypeConfiguration<CartoonProject>
    {
        /// <summary>Initializes a new instance of the <see cref="CartoonProjectMap"/> class.</summary>
        public CartoonProjectMap()
        {
            this.ToTable("CartoonProjects");

            this.Property(t => t.EachEpisodePlayingTime)
                .HasMaxLength(CartoonProject.EachEpisodePlayingTimeMaxLength);

            this.HasRequired(t => t.CartoonProjectFormat)
                .WithMany()
                .HasForeignKey(d => d.CartoonProjectFormatId);
        }
    }
}