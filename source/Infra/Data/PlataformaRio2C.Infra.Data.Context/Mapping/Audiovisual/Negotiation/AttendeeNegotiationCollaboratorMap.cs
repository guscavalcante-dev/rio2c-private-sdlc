// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 04-27-2022
//
// Last Modified By : Renan valentim
// Last Modified On : 06-26-2023
// ***********************************************************************
// <copyright file="AttendeeNegotiationCollaboratorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeNegotiationCollaboratorMap</summary>
    public class AttendeeNegotiationCollaboratorMap : EntityTypeConfiguration<AttendeeNegotiationCollaborator>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeNegotiationCollaboratorMap"/> class.</summary>
        public AttendeeNegotiationCollaboratorMap()
        {
            this.ToTable("AttendeeNegotiationCollaborators");

            // Relationships
            this.HasRequired(anc => anc.Negotiation)
                .WithMany(n => n.AttendeeNegotiationCollaborators)
                .HasForeignKey(anc => anc.NegotiationId);

            this.HasRequired(anc => anc.AttendeeCollaborator)
                .WithMany(ac => ac.AttendeeNegotiationCollaborators)
                .HasForeignKey(anc => anc.AttendeeCollaboratorId);
        }
    }
}