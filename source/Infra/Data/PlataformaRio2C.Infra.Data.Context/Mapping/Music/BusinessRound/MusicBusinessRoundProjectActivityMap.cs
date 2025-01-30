// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Daniel Giese Rodrigues
// Created          : 01-23-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-23-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectActivityMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>MusicBusinessRoundProjectActivityMap</summary>
    public class MusicBusinessRoundProjectActivityMap : EntityTypeConfiguration<MusicBusinessRoundProjectActivity>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundProjectActivityMap"/> class.</summary>
        public MusicBusinessRoundProjectActivityMap()
        {
            this.ToTable("MusicBusinessRoundProjectActivities");

            this.Property(t => t.AdditionalInfo)
                .HasMaxLength(MusicBusinessRoundProjectActivity.AdditionalInfoMaxLength);

            // Relationships
            this.HasRequired(t => t.MusicBusinessRoundProject)
                .WithMany(p => p.MusicBusinessRoundProjectActivities)
                .HasForeignKey(t => t.MusicBusinessRoundProjectId);

            this.HasRequired(t => t.Activity)
                .WithMany()
                .HasForeignKey(t => t.ActivityId);
        }
    }
}
