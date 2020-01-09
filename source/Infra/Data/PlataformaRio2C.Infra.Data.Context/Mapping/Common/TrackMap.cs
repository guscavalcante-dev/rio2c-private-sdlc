// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="TrackMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>TrackMap</summary>
    public class TrackMap : EntityTypeConfiguration<Track>
    {
        /// <summary>Initializes a new instance of the <see cref="TrackMap"/> class.</summary>
        public TrackMap()
        {
            this.ToTable("Tracks");

            this.Property(p => p.Name)
                .HasMaxLength(Track.NameMaxLength);

            this.Property(p => p.Color)
                .HasMaxLength(Track.ColorMaxLength);

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(t => t.EditionId);
        }
    }
}