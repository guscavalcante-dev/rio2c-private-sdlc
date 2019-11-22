// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="ProjectImageLinkMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectImageLinkMap</summary>
    public class ProjectImageLinkMap : EntityTypeConfiguration<ProjectImageLink>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectImageLinkMap"/> class.</summary>
        public ProjectImageLinkMap()
        {
            this.ToTable("ProjectImageLinks");

            Property(u => u.Value)
                .HasMaxLength(ProjectImageLink.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.ProjectImageLinks)
                .HasForeignKey(t => t.ProjectId);
        }
    }
}