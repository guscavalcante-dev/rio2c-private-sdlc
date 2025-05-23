﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="ProjectEvaluationRefuseReasonMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectEvaluationRefuseReasonMap</summary>
    public class ProjectEvaluationRefuseReasonMap : EntityTypeConfiguration<ProjectEvaluationRefuseReason>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectEvaluationRefuseReasonMap"/> class.</summary>
        public ProjectEvaluationRefuseReasonMap()
        {
            this.ToTable("ProjectEvaluationRefuseReasons");

            this.Property(u => u.Name)
                .HasMaxLength(ProjectEvaluationRefuseReason.NameMaxLength);

            // Relationships
            this.HasRequired(t => t.ProjectType)
                .WithMany()
                .HasForeignKey(d => d.ProjectTypeId);
        }
    }
}