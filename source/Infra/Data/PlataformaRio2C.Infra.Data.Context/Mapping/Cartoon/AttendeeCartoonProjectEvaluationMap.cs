// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Franco
// Created          : 02-04-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-04-2022
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectEvaluationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCartoonProjectEvaluationMap</summary>
    public class AttendeeCartoonProjectEvaluationMap : EntityTypeConfiguration<AttendeeCartoonProjectEvaluation>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCartoonProjectEvaluationMap"/> class.</summary>
        public AttendeeCartoonProjectEvaluationMap()
        {
            this.ToTable("AttendeeCartoonProjectEvaluations");
            this.Property(x => x.AttendeeCartoonProjectId).HasColumnName("AttendeeCartoonProjectId");

            // Relationships
            this.HasRequired(t => t.AttendeeCartoonProject)
                .WithMany(e => e.AttendeeCartoonProjectEvaluations)
                .HasForeignKey(d => d.AttendeeCartoonProjectId);

            this.HasRequired(t => t.EvaluatorUser)
                .WithMany(u => u.AttendeeCartoonProjectEvaluations)
                .HasForeignKey(d => d.EvaluatorUserId);
        }
    }
}