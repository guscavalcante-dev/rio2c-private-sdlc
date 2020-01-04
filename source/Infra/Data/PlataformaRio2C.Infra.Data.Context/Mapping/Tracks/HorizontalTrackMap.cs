// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="HorizontalTrackMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>HorizontalTrackMap</summary>
    public class HorizontalTrackMap : EntityTypeConfiguration<HorizontalTrack>
    {
        /// <summary>Initializes a new instance of the <see cref="HorizontalTrackMap"/> class.</summary>
        public HorizontalTrackMap()
        {
            this.ToTable("HorizontalTracks");

            this.Property(p => p.Name)
                .HasMaxLength(HorizontalTrack.NameMaxLength);
        }
    }
}