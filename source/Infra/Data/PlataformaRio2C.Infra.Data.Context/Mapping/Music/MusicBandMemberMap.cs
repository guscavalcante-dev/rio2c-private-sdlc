// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicBandMemberMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>MusicBandMemberMap</summary>
    public class MusicBandMemberMap : EntityTypeConfiguration<MusicBandMember>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBandMemberMap"/> class.</summary>
        public MusicBandMemberMap()
        {
            this.ToTable("MusicBandMembers");

            this.Property(t => t.Name)
                .HasMaxLength(MusicBandMember.NameMaxLength);

            this.Property(t => t.MusicInstrumentName)
                .HasMaxLength(MusicBandMember.MusicInstrumentNameMaxLength);

            // Relationships
            this.HasRequired(t => t.MusicBand)
                .WithMany(e => e.MusicBandMembers)
                .HasForeignKey(d => d.MusicBandId);
        }
    }
}