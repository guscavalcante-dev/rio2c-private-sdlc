// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 02-26-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-26-2024
// ***********************************************************************
// <copyright file="AttendeeCreatorProjectEvaluationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeCreatorProjectEvaluationMap</summary>
    public class AttendeeCreatorProjectEvaluationMap : EntityTypeConfiguration<AttendeeCreatorProjectEvaluation>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCreatorProjectEvaluationMap"/> class.</summary>
        public AttendeeCreatorProjectEvaluationMap()
        {
            this.ToTable("AttendeeCreatorProjectEvaluations");

            // Relationships
            this.HasRequired(t => t.AttendeeCreatorProject)
                .WithMany(aio => aio.AttendeeCreatorProjectEvaluations)
                .HasForeignKey(d => d.AttendeeCreatorProjectId);

            this.HasRequired(t => t.EvaluatorUser)
                .WithMany(u => u.AttendeeCreatorProjectEvaluations)
                .HasForeignKey(d => d.EvaluatorUserId);
        }
    }
}