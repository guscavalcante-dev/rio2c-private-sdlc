// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 11-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-11-2019
// ***********************************************************************
// <copyright file="ProjectEvaluationStatusMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectEvaluationStatusMap</summary>
    public class ProjectEvaluationStatusMap : EntityTypeConfiguration<ProjectEvaluationStatus>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectEvaluationStatusMap"/> class.</summary>
        public ProjectEvaluationStatusMap()
        {
            this.ToTable("ProjectEvaluationStatuses");

            Property(u => u.Name)
                .HasMaxLength(ProjectEvaluationStatus.NameMaxLength);

            Property(u => u.Code)
                .HasMaxLength(ProjectEvaluationStatus.CodeMaxLength);
        }
    }
}