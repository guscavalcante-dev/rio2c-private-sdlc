// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 11-08-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="ProjectTargetAudienceMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectTargetAudienceMap</summary>
    public class ProjectTargetAudienceMap : EntityTypeConfiguration<ProjectTargetAudience>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectTargetAudienceMap"/> class.</summary>
        public ProjectTargetAudienceMap()
        {
            this.ToTable("ProjectTargetAudiences");

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.ProjectTargetAudiences)
                .HasForeignKey(d => d.ProjectId);

            this.HasRequired(t => t.TargetAudience)
               .WithMany()
               .HasForeignKey(d => d.TargetAudienceId);
        }
    }
}