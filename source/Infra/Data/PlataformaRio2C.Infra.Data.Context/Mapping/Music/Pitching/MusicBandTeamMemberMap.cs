// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicBandTeamMemberMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>MusicBandTeamMemberMap</summary>
    public class MusicBandTeamMemberMap : EntityTypeConfiguration<MusicBandTeamMember>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBandTeamMemberMap"/> class.</summary>
        public MusicBandTeamMemberMap()
        {
            this.ToTable("MusicBandTeamMembers");

            this.Property(t => t.Name)
                .HasMaxLength(MusicBandTeamMember.NameMaxLength);

            this.Property(t => t.Role)
                .HasMaxLength(MusicBandTeamMember.RoleMaxLength);

            // Relationships
            this.HasRequired(t => t.MusicBand)
                .WithMany(e => e.MusicBandTeamMembers)
                .HasForeignKey(d => d.MusicBandId);
        }
    }
}