// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="ConferenceHorizontalTrackMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferenceHorizontalTrackMap</summary>
    public class ConferenceHorizontalTrackMap : EntityTypeConfiguration<ConferenceHorizontalTrack>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceHorizontalTrackMap"/> class.</summary>
        public ConferenceHorizontalTrackMap()
        {
            this.ToTable("ConferenceHorizontalTracks");

            //Property(u => u.Info)
            //   .HasMaxLength(Conference.InfoMaxLength);

            //Relationships 
            this.HasRequired(t => t.Conference)
              .WithMany(d => d.ConferenceHorizontalTracks)
              .HasForeignKey(t => t.ConferenceId);

            this.HasRequired(t => t.HorizontalTrack)
                .WithMany()
                .HasForeignKey(t => t.HorizontalTrackId);
        }
    }
}