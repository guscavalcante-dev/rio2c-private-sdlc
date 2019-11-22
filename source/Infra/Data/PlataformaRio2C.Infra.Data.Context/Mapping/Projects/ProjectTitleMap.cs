// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="ProjectTitleMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectTitleMap</summary>
    public class ProjectTitleMap : EntityTypeConfiguration<ProjectTitle>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectTitleMap"/> class.</summary>
        public ProjectTitleMap()
        {
            this.ToTable("ProjectTitles");

            //this.Ignore(p => p.LanguageCode);

            Property(u => u.Value)
               .HasMaxLength(ProjectTitle.ValueMaxLength);            

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.ProjectTitles)
                .HasForeignKey(t => t.ProjectId);
        }
    }
}