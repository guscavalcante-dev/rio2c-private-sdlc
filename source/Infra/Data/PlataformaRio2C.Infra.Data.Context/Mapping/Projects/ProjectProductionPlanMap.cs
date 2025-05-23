﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="ProjectProductionPlanMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectProductionPlanMap</summary>
    public class ProjectProductionPlanMap : EntityTypeConfiguration<ProjectProductionPlan>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectProductionPlanMap"/> class.</summary>
        public ProjectProductionPlanMap()
        {
            this.ToTable("ProjectProductionPlans");

            //this.Ignore(p => p.LanguageCode);

            Property(u => u.Value)
                //.HasColumnType("nvarchar(max)")
                .HasMaxLength(ProjectProductionPlan.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.ProjectProductionPlans)
                .HasForeignKey(t => t.ProjectId);
        }
    }
}