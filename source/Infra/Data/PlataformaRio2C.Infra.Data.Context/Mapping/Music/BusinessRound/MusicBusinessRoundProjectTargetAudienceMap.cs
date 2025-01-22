// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Daniel Giese Rodrigues
// Created          : 01-20-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-20-2025
// ***********************************************************************
// <copyright file="ProjectInterestMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectTargetAudienceMap</summary>
    public class MusicBusinessRoundProjectTargetAudienceMap : EntityTypeConfiguration<MusicBusinessRoundProjectTargetAudience>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectTargetAudienceMap"/> class.</summary>
        public MusicBusinessRoundProjectTargetAudienceMap()
        {
            this.ToTable("MusicBusinessRoundProjectTargetAudiences");

            // AdditionInfoMaxLenght
            this.Property(t => t.AdditionalInfo)
                .HasMaxLength(MusicBusinessRoundProjectTargetAudience.AdditionalInfoMaxLength);

            //Relationships
            this.HasRequired(t => t.MusicBusinessRoundProject)
                .WithMany(e => e.MusicBusinessRoundProjectTargetAudience)
                .HasForeignKey(d => d.MusicBusinessRoundProjectId);

            this.HasRequired(t => t.TargetAudience)
               .WithMany()
               .HasForeignKey(d => d.TargetAudienceId);
        }
    }
}
