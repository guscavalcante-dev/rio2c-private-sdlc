﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicBandMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>MusicBandMap</summary>
    public class MusicBandMap : EntityTypeConfiguration<MusicBand>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBandMap"/> class.</summary>
        public MusicBandMap()
        {
            this.ToTable("MusicBands");

            this.Property(t => t.Name)
                .HasMaxLength(MusicBand.NameMaxLength);

            this.Property(t => t.FormationDate)
                .HasMaxLength(MusicBand.FormationDateMaxLength);

            this.Property(t => t.MainMusicInfluences)
                .HasMaxLength(MusicBand.MainMusicInfluencesMaxLength);

            this.Property(t => t.Facebook)
                .HasMaxLength(MusicBand.FacebookMaxLength);

            this.Property(t => t.Instagram)
                .HasMaxLength(MusicBand.InstagramMaxLength);

            this.Property(t => t.Twitter)
                .HasMaxLength(MusicBand.TwitterMaxLength);

            this.Property(t => t.Youtube)
                .HasMaxLength(MusicBand.YoutubeMaxLength);

            this.Property(t => t.Tiktok)
                .HasMaxLength(MusicBand.TiktokMaxLength);

            this.Property(t => t.Spotify)
               .HasMaxLength(MusicBand.SpotifyMaxLength);

            this.Property(t => t.Deezer)
              .HasMaxLength(MusicBand.DeezerMaxLength);

            // Relationships
            this.HasRequired(t => t.MusicBandType)
                .WithMany()
                .HasForeignKey(d => d.MusicBandTypeId);
        }
    }
}