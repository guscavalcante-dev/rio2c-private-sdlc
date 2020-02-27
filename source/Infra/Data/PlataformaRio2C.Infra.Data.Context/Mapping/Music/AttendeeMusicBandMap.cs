// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-26-2020
// ***********************************************************************
// <copyright file="AttendeeMusicBandMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeMusicBandMap</summary>
    public class AttendeeMusicBandMap : EntityTypeConfiguration<AttendeeMusicBand>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBandMap"/> class.</summary>
        public AttendeeMusicBandMap()
        {
            this.ToTable("AttendeeMusicBands");

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(d => d.EditionId);

            this.HasRequired(t => t.MusicBand)
                .WithMany(e => e.AttendeeMusicBands)
                .HasForeignKey(d => d.MusicBandId);

            //this.HasMany(t => t.AttendeeOrganizationCollaborators)
            //    .WithRequired(e => e.AttendeeOrganization)
            //    .HasForeignKey(e => e.AttendeeOrganizationId);
        }
    }
}