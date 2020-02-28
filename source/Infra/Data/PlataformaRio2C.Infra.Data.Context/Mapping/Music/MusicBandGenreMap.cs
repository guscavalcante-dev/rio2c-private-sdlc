// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicBandGenreMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>MusicBandGenreMap</summary>
    public class MusicBandGenreMap : EntityTypeConfiguration<MusicBandGenre>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBandGenreMap"/> class.</summary>
        public MusicBandGenreMap()
        {
            this.ToTable("MusicBandGenres");

            this.Property(t => t.AdditionalInfo)
                .HasMaxLength(MusicBandGenre.AdditionalInfoMaxLength);

            //Relationships
            this.HasRequired(t => t.MusicBand)
                .WithMany(e => e.MusicBandGenres)
                .HasForeignKey(d => d.MusicBandId);

            this.HasRequired(t => t.MusicGenre)
               .WithMany()
               .HasForeignKey(d => d.MusicGenreId);
        }
    }
}