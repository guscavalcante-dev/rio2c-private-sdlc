// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicBandTargetAudienceMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>MusicBandTargetAudienceMap</summary>
    public class MusicBandTargetAudienceMap : EntityTypeConfiguration<MusicBandTargetAudience>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBandTargetAudienceMap"/> class.</summary>
        public MusicBandTargetAudienceMap()
        {
            this.ToTable("MusicBandTargetAudiences");

            //Relationships
            this.HasRequired(t => t.MusicBand)
                .WithMany(e => e.MusicBandTargetAudiences)
                .HasForeignKey(d => d.MusicBandId);

            this.HasRequired(t => t.TargetAudience)
               .WithMany()
               .HasForeignKey(d => d.TargetAudienceId);
        }
    }
}