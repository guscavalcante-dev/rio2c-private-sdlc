// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 25-02-2025
//
// Last Modified By :Renan Valentim
// Last Modified On :25-02-2025
// ***********************************************************************
// <copyright file="AttendeeMusicBusinessRoundNegotiationCollaboratorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeMusicBusinessRoundNegotiationCollaboratorMap</summary>
    public class AttendeeMusicBusinessRoundNegotiationCollaboratorMap : EntityTypeConfiguration<AttendeeMusicBusinessRoundNegotiationCollaborator>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBusinessRoundNegotiationCollaboratorMap"/> class.</summary>
        public AttendeeMusicBusinessRoundNegotiationCollaboratorMap()
        {
            this.ToTable("AttendeeMusicBusinessRoundNegotiationCollaborators");

            // Relationships
            this.HasRequired(anc => anc.MusicBusinessRoundNegotiation)
                .WithMany(n => n.AttendeeMusicBusinessRoundNegotiationCollaborators)
                .HasForeignKey(anc => anc.MusicBusinessRoundNegotiationId);

            this.HasRequired(anc => anc.AttendeeCollaborator)
                .WithMany(ac => ac.AttendeeMusicBusinessRoundNegotiationCollaborators)
                .HasForeignKey(anc => anc.AttendeeCollaboratorId);
        }
    }
}