// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationOptionMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>InnovationOrganizationOptionMap</summary>
    public class InnovationOrganizationOptionMap : EntityTypeConfiguration<InnovationOrganizationOption>
    {
        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationOptionMap"/> class.</summary>
        public InnovationOrganizationOptionMap()
        {
            this.ToTable("InnovationOrganizationOptions");

            // Relationships
            this.HasRequired(t => t.InnovationOrganization)
                .WithMany(io => io.InnovationOrganizationOptions)
                .HasForeignKey(d => d.InnovationOrganizationId);

            this.HasRequired(t => t.InnovationOption)
                .WithMany()
                .HasForeignKey(d => d.InnovationOptionId);
        }
    }
}