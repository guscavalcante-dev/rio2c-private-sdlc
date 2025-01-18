// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-26-2020
// ***********************************************************************
// <copyright file="AttendeeMusicBandCollaboratorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeMusicBandCollaboratorMap</summary>
    public class AttendeeMusicBandCollaboratorMap : EntityTypeConfiguration<AttendeeMusicBandCollaborator>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBandCollaboratorMap"/> class.</summary>
        public AttendeeMusicBandCollaboratorMap()
        {
            this.ToTable("AttendeeMusicBandCollaborators");

            // Relationships
            this.HasRequired(t => t.AttendeeMusicBand)
                .WithMany(e => e.AttendeeMusicBandCollaborators)
                .HasForeignKey(d => d.AttendeeMusicBandId);

            this.HasRequired(t => t.AttendeeCollaborator)
                .WithMany(e => e.AttendeeMusicBandCollaborators)
                .HasForeignKey(d => d.AttendeeCollaboratorId);
        }
    }
}