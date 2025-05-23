﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-21-2025
// ***********************************************************************
// <copyright file="ProjectMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectMap</summary>
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectMap"/> class.</summary>
        public ProjectMap()
        {
            this.ToTable("Projects");

            this.Property(t => t.TotalPlayingTime)
                .HasMaxLength(Project.TotalPlayingTimeMaxLength)
                .IsRequired();

            this.Property(t => t.EachEpisodePlayingTime)
                .HasMaxLength(Project.EachEpisodePlayingTimeMaxLength);

            this.Property(t => t.ValuePerEpisode)
                .HasMaxLength(Project.ValuePerEpisodeMaxLength);

            this.Property(t => t.TotalValueOfProject)
                .HasMaxLength(Project.TotalValueOfProjectMaxLength);

            this.Property(t => t.ValueAlreadyRaised)
                .HasMaxLength(Project.ValueAlreadyRaisedMaxLength);

            this.Property(t => t.ValueStillNeeded)
                .HasMaxLength(Project.ValueStillNeededMaxLength);

            this.Property(t => t.WhichTypeOfFinancingDescription)
                .HasMaxLength(Project.WhichTypeOfFinancingDescriptionMaxLength);

            this.Property(t => t.PitchingJsonPayload)
                .HasMaxLength(Project.PitchingJsonPayloadMaxLength);

            //Relationships
            this.HasRequired(t => t.SellerAttendeeOrganization)
                .WithMany(t => t.SellProjects)
                .HasForeignKey(t => t.SellerAttendeeOrganizationId);

            this.HasMany(t => t.ProjectTitles)
                  .WithRequired(t => t.Project)
                  .HasForeignKey(t => t.ProjectId);
        }
    }
}