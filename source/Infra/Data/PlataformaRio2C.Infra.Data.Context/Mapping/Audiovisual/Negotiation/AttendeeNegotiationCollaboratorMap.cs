// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 04-27-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-27-2022
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
            this.HasRequired(t => t.Negotiation)
                .WithMany()
                .HasForeignKey(t => t.NegotiationId);

            this.HasRequired(t => t.AttendeeCollaborator)
                .WithMany()
                .HasForeignKey(t => t.AttendeeCollaboratorId);
        }
    }
}