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
    /// <summary>ProjectInterestMap</summary>
    public class MusicBusinessRoundProjectInterestMap : EntityTypeConfiguration<MusicBusinessRoundProjectInterest>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectInterestMap"/> class.</summary>
        public MusicBusinessRoundProjectInterestMap()
        {
            this.ToTable("MusicBusinessRoundProjectInterest");

            this.Property(t => t.AdditionalInfo)
                .HasMaxLength(OrganizationActivity.AdditionalInfoMaxLength);

            //Relationships
            this.HasRequired(t => t.MusicBusinessRoundProject)
                .WithMany(p => p.MusicBusinessRoundProjectInterests)
                .HasForeignKey(t => t.MusicBusinessRoundProject);

            this.HasRequired(t => t.Interest)
                .WithMany()
                .HasForeignKey(d => d.InterestId);
        }
    }
}

