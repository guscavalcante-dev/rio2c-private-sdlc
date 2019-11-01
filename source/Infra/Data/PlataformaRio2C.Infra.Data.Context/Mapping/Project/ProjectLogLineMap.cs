// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-01-2019
// ***********************************************************************
// <copyright file="ProjectLogLineMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectLogLineMap</summary>
    public class ProjectLogLineMap : EntityTypeConfiguration<ProjectLogLine>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectLogLineMap"/> class.</summary>
        public ProjectLogLineMap()
        {
            this.ToTable("ProjectLogLines");

            //this.Ignore(p => p.LanguageCode);

            Property(u => u.Value)
                .HasMaxLength(8000);

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.LogLines)
                .HasForeignKey(t => t.ProjectId);
        }
    }
}