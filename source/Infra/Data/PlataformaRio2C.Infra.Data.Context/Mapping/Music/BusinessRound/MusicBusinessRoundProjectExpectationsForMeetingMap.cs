// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Daniel Giese Rodrigues
// Created          : 01-20-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-20-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectExpectationsForMeetingMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectMap</summary>
    public class MusicBusinessRoundProjectExpectationsForMeetingMap : EntityTypeConfiguration<MusicBusinessRoundProjectExpectationsForMeeting>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundProjectExpectationsForMeetingMap"/> class.</summary>
        public MusicBusinessRoundProjectExpectationsForMeetingMap()
        {
            this.ToTable("MusicBusinessRoundProjectExpectationsForMeetings");
            this.HasKey(t => t.Id);

            // Max lengths
            Property(u => u.Value)
                .HasMaxLength(MusicBusinessRoundProjectExpectationsForMeeting.ValueMaxLength);

            // Relationships
            this.HasRequired(t => t.MusicBusinessRoundProject)
                .WithMany(e => e.MusicBusinessRoundProjectExpectationsForMeetings)
                .HasForeignKey(t => t.MusicBusinessRoundProjectId);

            this.HasRequired(t => t.Language)
                .WithMany()
                .HasForeignKey(d => d.LanguageId);
        }
    }
}