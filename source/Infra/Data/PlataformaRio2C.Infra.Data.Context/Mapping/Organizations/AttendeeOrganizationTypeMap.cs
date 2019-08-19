// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationTypeMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeOrganizationTypeMap</summary>
    public class AttendeeOrganizationTypeMap : EntityTypeConfiguration<AttendeeOrganizationType>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationMap"/> class.</summary>
        public AttendeeOrganizationTypeMap()
        {
            this.ToTable("AttendeeOrganizationTypes");

            // Relationships
            this.HasRequired(t => t.OrganizationType)
                .WithMany(e => e.AttendeeOrganizationTypes)
                .HasForeignKey(d => d.OrganizationTypeId);

            this.HasRequired(t => t.AttendeeOrganization)
                .WithMany(e => e.AttendeeOrganizationTypes)
                .HasForeignKey(d => d.AttendeeOrganizationId);

            //this.HasRequired(t => t.Organization)
            //    .WithMany(e => e.AttendeeOrganizations)
            //    .HasForeignKey(d => d.OrganizationId);
        }
    }
}