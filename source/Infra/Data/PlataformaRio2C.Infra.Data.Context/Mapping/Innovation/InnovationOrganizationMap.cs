// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>InnovationOrganizationMap</summary>
    public class InnovationOrganizationMap : EntityTypeConfiguration<InnovationOrganization>
    {
        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationMap"/> class.</summary>
        public InnovationOrganizationMap()
        {
            this.ToTable("InnovationOrganizations");

            // Relationships
            //this.HasRequired(t => t.Edition)
            //    .WithMany(e => e.InnovationOrganizations)
            //    .HasForeignKey(d => d.EditionId);

            //this.HasRequired(t => t.Organization)
            //    .WithMany(e => e.InnovationOrganizations)
            //    .HasForeignKey(d => d.OrganizationId);
        }
    }
}