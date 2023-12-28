// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 12-27-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-27-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorActivityMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCollaboratorActivityMap</summary>
    public class AttendeeCollaboratorActivityMap : EntityTypeConfiguration<AttendeeCollaboratorActivity>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorActivityMap"/> class.</summary>
        public AttendeeCollaboratorActivityMap()
        {
            this.ToTable("AttendeeCollaboratorActivities");

            // Relationships
            this.HasRequired(caci => caci.Activity)
                .WithMany()
                .HasForeignKey(caci => caci.ActivityId);

            this.HasRequired(caci => caci.AttendeeCollaborator)
                .WithMany(ac => ac.AttendeeCollaboratorActivities)
                .HasForeignKey(caci => caci.AttendeeCollaboratorId);
        }
    }
}