// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="InnovationOptionGroupMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>InnovationOptionGroupMap</summary>
    public class InnovationOptionGroupMap : EntityTypeConfiguration<InnovationOptionGroup>
    {
        /// <summary>Initializes a new instance of the <see cref="InnovationOptionGroupMap"/> class.</summary>
        public InnovationOptionGroupMap()
        {
            this.ToTable("InnovationOptionGroups");

            // Relationships
            //this.HasRequired(t => t.Edition)
            //    .WithMany(e => e.InnovationOptionGroups)
            //    .HasForeignKey(d => d.EditionId);

            //this.HasRequired(t => t.Organization)
            //    .WithMany(e => e.InnovationOptionGroups)
            //    .HasForeignKey(d => d.OrganizationId);
        }
    }
}