// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="ConferenceVerticalTrackMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferenceVerticalTrackMap</summary>
    public class ConferenceVerticalTrackMap : EntityTypeConfiguration<ConferenceVerticalTrack>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceVerticalTrackMap"/> class.</summary>
        public ConferenceVerticalTrackMap()
        {
            this.ToTable("ConferenceVerticalTracks");

            //Property(u => u.Info)
            //   .HasMaxLength(Conference.InfoMaxLength);

            //Relationships 
            this.HasRequired(t => t.Conference)
              .WithMany(d => d.ConferenceVerticalTracks)
              .HasForeignKey(t => t.ConferenceId);

            this.HasRequired(t => t.VerticalTrack)
                .WithMany(d => d.ConferenceVerticalTracks)
                .HasForeignKey(t => t.VerticalTrackId);
        }
    }
}