// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="VerticalTrackMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>VerticalTrackMap</summary>
    public class VerticalTrackMap : EntityTypeConfiguration<VerticalTrack>
    {
        /// <summary>Initializes a new instance of the <see cref="VerticalTrackMap"/> class.</summary>
        public VerticalTrackMap()
        {
            this.ToTable("VerticalTracks");

            this.Property(p => p.Name)
                .HasMaxLength(VerticalTrack.NameMaxLength);
        }
    }
}