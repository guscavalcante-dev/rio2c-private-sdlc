// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 02-26-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-26-2024
// ***********************************************************************
// <copyright file="CreatorProjectInterestMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CreatorProjectInterestMap</summary>
    public class CreatorProjectInterestMap : EntityTypeConfiguration<CreatorProjectInterest>
    {
        /// <summary>Initializes a new instance of the <see cref="CreatorProjectInterestMap"/> class.</summary>
        public CreatorProjectInterestMap()
        {
            this.ToTable("CreatorProjectInterests");

            // Relationships
            this.HasRequired(t => t.CreatorProject)
                .WithMany()
                .HasForeignKey(d => d.CreatorProjectId);

            this.HasRequired(t => t.Interest)
                   .WithMany()
                   .HasForeignKey(d => d.InterestId);
        }
    }
}