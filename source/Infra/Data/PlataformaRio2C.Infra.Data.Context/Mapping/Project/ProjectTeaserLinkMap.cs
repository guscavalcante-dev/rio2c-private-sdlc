// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-01-2019
// ***********************************************************************
// <copyright file="ProjectTeaserLinkMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectTeaserLinkMap</summary>
    public class ProjectTeaserLinkMap : EntityTypeConfiguration<ProjectTeaserLink>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectTeaserLinkMap"/> class.</summary>
        public ProjectTeaserLinkMap()
        {
            this.ToTable("ProjectTeaserLinks");

            Property(u => u.Value)
                .HasMaxLength(ProjectTeaserLink.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.TeaserLinks)
                .HasForeignKey(t => t.ProjectId);
        }
    }
}