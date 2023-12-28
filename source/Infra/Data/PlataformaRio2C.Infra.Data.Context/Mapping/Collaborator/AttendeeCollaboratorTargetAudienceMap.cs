// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 12-27-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-27-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTargetAudienceMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCollaboratorTargetAudienceMap</summary>
    public class AttendeeCollaboratorTargetAudienceMap : EntityTypeConfiguration<AttendeeCollaboratorTargetAudience>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTargetAudienceMap"/> class.</summary>
        public AttendeeCollaboratorTargetAudienceMap()
        {
            this.ToTable("AttendeeCollaboratorTargetAudiences");

            // Relationships
            this.HasRequired(caci => caci.TargetAudience)
                .WithMany()
                .HasForeignKey(caci => caci.TargetAudienceId);

            this.HasRequired(caci => caci.AttendeeCollaborator)
                .WithMany(ac => ac.AttendeeCollaboratorTargetAudiences)
                .HasForeignKey(caci => caci.AttendeeCollaboratorId);
        }
    }
}