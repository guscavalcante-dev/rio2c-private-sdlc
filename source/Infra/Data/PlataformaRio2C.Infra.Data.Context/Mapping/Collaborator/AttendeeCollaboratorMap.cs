// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCollaboratorMap</summary>
    public class AttendeeCollaboratorMap : EntityTypeConfiguration<AttendeeCollaborator>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorMap"/> class.</summary>
        public AttendeeCollaboratorMap()
        {
            this.ToTable("AttendeeCollaborators");

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany(e => e.AttendeeCollaborators)
                .HasForeignKey(d => d.EditionId);

            this.HasRequired(t => t.Collaborator)
                .WithMany(e => e.AttendeeCollaborators)
                .HasForeignKey(d => d.CollaboratorId);

            this.HasMany(t => t.AttendeeOrganizationCollaborators)
                .WithRequired(e => e.AttendeeCollaborator)
                .HasForeignKey(e => e.AttendeeCollaboratorId);
        }
    }
}