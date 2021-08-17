// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 08-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-17-2021
// ***********************************************************************
// <copyright file="CommissionEvaluationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CommissionEvaluationMap</summary>
    public class CommissionEvaluationMap : EntityTypeConfiguration<CommissionEvaluation>
    {
        /// <summary>Initializes a new instance of the <see cref="CommissionEvaluationMap"/> class.</summary>
        public CommissionEvaluationMap()
        {
            this.ToTable("CommissionEvaluations");

            // Relationships
            this.HasRequired(ce => ce.Project)
                .WithMany(p => p.CommissionEvaluations)
                .HasForeignKey(ce => ce.ProjectId);

            this.HasRequired(ce => ce.EvaluatorUser)
                .WithMany(u => u.CommissionEvaluations)
                .HasForeignKey(ce => ce.EvaluatorUserId);
        }
    }
}