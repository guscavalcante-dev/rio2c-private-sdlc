// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationCollaboratorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeOrganizationCollaboratorMap</summary>
    public class AttendeeOrganizationCollaboratorMap : EntityTypeConfiguration<AttendeeOrganizationCollaborator>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationCollaboratorMap"/> class.</summary>
        public AttendeeOrganizationCollaboratorMap()
        {
            this.ToTable("AttendeeOrganizationCollaborators");

            // Relationships
            this.HasRequired(t => t.AttendeeOrganization)
                .WithMany(e => e.AttendeeOrganizationCollaborators)
                .HasForeignKey(d => d.AttendeeOrganizationId);

            this.HasRequired(t => t.AttendeeCollaborator)
                .WithMany(e => e.AttendeeOrganizationCollaborators)
                .HasForeignKey(d => d.AttendeeCollaboratorId);
        }
    }
}