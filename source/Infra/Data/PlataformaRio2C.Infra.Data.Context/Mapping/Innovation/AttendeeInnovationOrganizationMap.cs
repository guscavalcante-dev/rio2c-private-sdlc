// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationMap</summary>
    public class AttendeeInnovationOrganizationMap : EntityTypeConfiguration<AttendeeInnovationOrganization>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationMap"/> class.</summary>
        public AttendeeInnovationOrganizationMap()
        {
            this.ToTable("AttendeeInnovationOrganizations");

            // Relationships
            //this.HasRequired(t => t.Edition)
            //    .WithMany(e => e.AttendeeInnovationOrganizations)
            //    .HasForeignKey(d => d.EditionId);

            //this.HasRequired(t => t.Organization)
            //    .WithMany(e => e.AttendeeInnovationOrganizations)
            //    .HasForeignKey(d => d.OrganizationId);
        }
    }
}