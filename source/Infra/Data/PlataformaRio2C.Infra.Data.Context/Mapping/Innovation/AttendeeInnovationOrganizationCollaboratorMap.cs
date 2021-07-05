// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationCollaboratorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationCollaboratorMap</summary>
    public class AttendeeInnovationOrganizationCollaboratorMap : EntityTypeConfiguration<AttendeeInnovationOrganizationCollaborator>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationCollaboratorMap"/> class.</summary>
        public AttendeeInnovationOrganizationCollaboratorMap()
        {
            this.ToTable("AttendeeInnovationOrganizationCollaborators");

            // Relationships
            this.HasRequired(t => t.AttendeeInnovationOrganization)
                .WithMany(aio => aio.AttendeeInnovationOrganizationCollaborators)
                .HasForeignKey(d => d.AttendeeInnovationOrganizationId);

            this.HasRequired(t => t.AttendeeCollaborator)
                .WithMany(ac => ac.AttendeeInnovationOrganizationCollaborators)
                .HasForeignKey(d => d.AttendeeCollaboratorId);
        }
    }
}