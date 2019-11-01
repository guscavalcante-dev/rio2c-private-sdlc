// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-01-2019
// ***********************************************************************
// <copyright file="ProjectSummaryMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectSummaryMap</summary>
    public class ProjectSummaryMap : EntityTypeConfiguration<ProjectSummary>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectSummaryMap"/> class.</summary>
        public ProjectSummaryMap()
        {
            this.ToTable("ProjectSummaries");

            //this.Ignore(p => p.LanguageCode);

            Property(u => u.Value)
                .HasColumnType("nvarchar(max)")
                .HasMaxLength(int.MaxValue);

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.Summaries)
                .HasForeignKey(t => t.ProjectId);
        }
    }
}