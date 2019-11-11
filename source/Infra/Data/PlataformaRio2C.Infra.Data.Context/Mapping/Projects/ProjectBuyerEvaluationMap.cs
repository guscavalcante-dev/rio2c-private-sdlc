// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-11-2019
// ***********************************************************************
// <copyright file="ProjectBuyerEvaluationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectBuyerEvaluationMap</summary>
    public class ProjectBuyerEvaluationMap : EntityTypeConfiguration<ProjectBuyerEvaluation>
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectBuyerEvaluationMap"/> class.</summary>
        public ProjectBuyerEvaluationMap()
        {
            this.ToTable("ProjectBuyerEvaluations");

            Property(u => u.Reason)
                .HasMaxLength(ProjectBuyerEvaluation.ReasonMaxLength);

            //Relationships
            this.HasRequired(t => t.Project)
                .WithMany(e => e.BuyerEvaluations)
                .HasForeignKey(d => d.ProjectId);

            this.HasRequired(t => t.BuyerAttendeeOrganization)
                .WithMany(e => e.ProjectBuyerEvaluations)
                .HasForeignKey(d => d.BuyerAttendeeOrganizationId);

            this.HasRequired(t => t.SellerUser)
                .WithMany()
                .HasForeignKey(d => d.SellerUserId);

            this.HasRequired(t => t.BuyerEvaluationUser)
                .WithMany()
                .HasForeignKey(d => d.BuyerEvaluationUserId);

            this.HasRequired(t => t.ProjectEvaluationStatus)
                .WithMany()
                .HasForeignKey(d => d.ProjectEvaluationStatusId);
        }
    }
}