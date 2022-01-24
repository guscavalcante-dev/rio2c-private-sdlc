// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="CartoonProjectProductionPlanMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CartoonProjectProductionPlanMap</summary>
    public class CartoonProjectProductionPlanMap : EntityTypeConfiguration<CartoonProjectProductionPlan>
    {
        /// <summary>Initializes a new instance of the <see cref="CartoonProjectProductionPlanMap"/> class.</summary>
        public CartoonProjectProductionPlanMap()
        {
            this.ToTable("CartoonProjectProductionPlans");

            //this.Ignore(p => p.LanguageCode);

            Property(u => u.Value)
                //.HasColumnType("nvarchar(max)")
                .HasMaxLength(CartoonProjectProductionPlan.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.CartoonProject)
                .WithMany(e => e.CartoonProjectProductionPlans)
                .HasForeignKey(t => t.CartoonProjectId);
        }
    }
}