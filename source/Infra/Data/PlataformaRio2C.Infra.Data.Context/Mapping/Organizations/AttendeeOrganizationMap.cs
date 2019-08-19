// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 08-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeOrganizationMap</summary>
    public class AttendeeOrganizationMap : EntityTypeConfiguration<AttendeeOrganization>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationMap"/> class.</summary>
        public AttendeeOrganizationMap()
        {
            this.ToTable("AttendeeOrganizations");

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany(e => e.AttendeeOrganizations)
                .HasForeignKey(d => d.EditionId);

            this.HasRequired(t => t.Organization)
                .WithMany(e => e.AttendeeOrganizations)
                .HasForeignKey(d => d.OrganizationId);

            this.HasMany(t => t.AttendeeOrganizationTypes)
                .WithRequired(e => e.AttendeeOrganization)
                .HasForeignKey(e => e.AttendeeOrganizationId);
        }
    }
}