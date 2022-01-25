// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="CartoonProjectMotivationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CartoonProjectMotivationMap</summary>
    public class CartoonProjectMotivationMap : EntityTypeConfiguration<CartoonProjectMotivation>
    {
        /// <summary>Initializes a new instance of the <see cref="CartoonProjectMotivationMap"/> class.</summary>
        public CartoonProjectMotivationMap()
        {
            this.ToTable("CartoonProjectMotivations");

            //this.Ignore(p => p.LanguageCode);

            Property(u => u.Value)
                .HasColumnType("nvarchar(max)")
                .HasMaxLength(int.MaxValue);

            //Relationships
            this.HasRequired(t => t.CartoonProject)
                .WithMany(e => e.CartoonProjectMotivations)
                .HasForeignKey(t => t.CartoonProjectId);
        }
    }
}