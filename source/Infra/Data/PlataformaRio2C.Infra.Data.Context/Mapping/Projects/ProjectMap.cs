// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
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

            //Relationships
            this.HasRequired(t => t.SellerAttendeeOrganization)
                .WithMany(e => e.SellProjects)
                .HasForeignKey(d => d.SellerAttendeeOrganizationId);

            this.HasMany(c => c.ProjectTitles)
                  .WithRequired(t => t.Project)
                  .HasForeignKey(t => t.ProjectId);
        }
    }
}