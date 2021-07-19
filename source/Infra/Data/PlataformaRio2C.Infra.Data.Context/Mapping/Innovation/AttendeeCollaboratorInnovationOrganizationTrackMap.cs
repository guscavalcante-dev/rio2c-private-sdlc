// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-16-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-16-2021
// ***********************************************************************
// <copyright file="AttendeeCollaboratorInnovationOrganizationTrackMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCollaboratorInnovationOrganizationTrackMap</summary>
    public class AttendeeCollaboratorInnovationOrganizationTrackMap : EntityTypeConfiguration<AttendeeCollaboratorInnovationOrganizationTrack>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorInnovationOrganizationTrackMap"/> class.</summary>
        public AttendeeCollaboratorInnovationOrganizationTrackMap()
        {
            this.ToTable("AttendeeCollaboratorInnovationOrganizationTracks");

            // Relationships
            this.HasRequired(t => t.InnovationOrganizationTrackOption)
                .WithMany()
                .HasForeignKey(d => d.InnovationOrganizationTrackOptionId);

            this.HasRequired(t => t.AttendeeCollaborator)
                .WithMany(ac => ac.AttendeeCollaboratorInnovationOrganizationTracks)
                .HasForeignKey(d => d.AttendeeCollaboratorId);
        }
    }
}