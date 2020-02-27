// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 02-26-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-26-2020
// ***********************************************************************
// <copyright file="MusicProjectMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>MusicProjectMap</summary>
    public class MusicProjectMap : EntityTypeConfiguration<MusicProject>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicProjectMap"/> class.</summary>
        public MusicProjectMap()
        {
            this.ToTable("MusicProjects");

            this.Property(t => t.VideoUrl)
                .HasMaxLength(MusicProject.VideoUrlMaxLength);

            this.Property(t => t.Music1Url)
                .HasMaxLength(MusicProject.Music1UrlMaxLength);

            this.Property(t => t.Music2Url)
                .HasMaxLength(MusicProject.Music2UrlMaxLength);

            this.Property(t => t.Reason)
                .HasMaxLength(MusicProject.ReasonMaxLength);

            // Relationships
            this.HasRequired(t => t.AttendeeMusicBand)
                .WithMany(e => e.MusicProjects)
                .HasForeignKey(t => t.AttendeeMusicBandId);

            this.HasRequired(t => t.ProjectEvaluationStatus)
                .WithMany()
                .HasForeignKey(t => t.ProjectEvaluationStatusId);

            this.HasOptional(t => t.ProjectEvaluationRefuseReason)
                .WithMany()
                .HasForeignKey(t => t.ProjectEvaluationRefuseReasonId);

            this.HasOptional(t => t.EvaluationUser)
                .WithMany()
                .HasForeignKey(t => t.EvaluationUserId);
        }
    }
}