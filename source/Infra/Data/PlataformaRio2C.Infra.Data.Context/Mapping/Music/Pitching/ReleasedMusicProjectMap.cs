// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="ReleasedMusicProjectMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ReleasedMusicProjectMap</summary>
    public class ReleasedMusicProjectMap : EntityTypeConfiguration<ReleasedMusicProject>
    {
        /// <summary>Initializes a new instance of the <see cref="ReleasedMusicProjectMap"/> class.</summary>
        public ReleasedMusicProjectMap()
        {
            this.ToTable("ReleasedMusicProjects");

            this.Property(t => t.Name)
                .HasMaxLength(ReleasedMusicProject.NameMaxLength);

            this.Property(t => t.Year)
                .HasMaxLength(ReleasedMusicProject.YearMaxLength);

            // Relationships
            this.HasRequired(t => t.MusicBand)
                .WithMany(e => e.ReleasedMusicProjects)
                .HasForeignKey(d => d.MusicBandId);
        }
    }
}