// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferenceParticipantMap</summary>
    public class ConferenceParticipantMap : EntityTypeConfiguration<ConferenceParticipant>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantMap"/> class.</summary>
        public ConferenceParticipantMap()
        {
            this.ToTable("ConferenceParticipants");

            //Relationships 
            this.HasRequired(t => t.Conference)
              .WithMany(d => d.ConferenceParticipants)
              .HasForeignKey(t => t.ConferenceId);

            this.HasRequired(t => t.AttendeeCollaborator)
                .WithMany()
                .HasForeignKey(t => t.AttendeeCollaboratorId);

            this.HasRequired(t => t.ConferenceParticipantRole)
                .WithMany(d => d.ConferenceParticipants)
                .HasForeignKey(t => t.ConferenceParticipantRoleId);
        }
    }
}