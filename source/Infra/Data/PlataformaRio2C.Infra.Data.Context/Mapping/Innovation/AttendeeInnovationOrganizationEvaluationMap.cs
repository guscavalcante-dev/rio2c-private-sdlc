// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-16-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-16-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationEvaluationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationEvaluationMap</summary>
    public class AttendeeInnovationOrganizationEvaluationMap : EntityTypeConfiguration<AttendeeInnovationOrganizationEvaluation>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationEvaluationMap"/> class.</summary>
        public AttendeeInnovationOrganizationEvaluationMap()
        {
            this.ToTable("AttendeeInnovationOrganizationEvaluations");

            // Relationships
            this.HasRequired(t => t.AttendeeInnovationOrganization)
                .WithMany(aio => aio.AttendeeInnovationOrganizationEvaluations)
                .HasForeignKey(d => d.AttendeeInnovationOrganizationId);

            this.HasRequired(t => t.EvaluatorUser)
                .WithMany(u => u.AttendeeInnovationOrganizationEvaluations)
                .HasForeignKey(d => d.EvaluatorUserId);
        }
    }
}