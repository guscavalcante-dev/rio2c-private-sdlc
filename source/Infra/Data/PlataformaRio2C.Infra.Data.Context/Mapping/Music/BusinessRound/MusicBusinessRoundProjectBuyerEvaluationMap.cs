// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Daniel Giese Rodrigues
// Created          : 01-20-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-20-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundProjectBuyerEvaluationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ProjectMap</summary>
    public class MusicBusinessRoundProjectBuyerEvaluationMap : EntityTypeConfiguration<MusicBusinessRoundProjectBuyerEvaluation>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundProjectBuyerEvaluationMap"/> class.</summary>
        public MusicBusinessRoundProjectBuyerEvaluationMap()
        {
            this.ToTable("MusicBusinessRoundProjectBuyerEvaluations");

            // Max lengths
            Property(u => u.Reason)
               .HasMaxLength(MusicBusinessRoundProjectBuyerEvaluation.ReasonMaxLength);

            // Relationships
            this.HasRequired(t => t.MusicBusinessRoundProject)
                .WithMany(e => e.MusicBusinessRoundProjectBuyerEvaluations)
                .HasForeignKey(t => t.MusicBusinessRoundProjectId);

            this.HasRequired(t => t.BuyerAttendeeOrganization)
                .WithMany(e => e.MusicBusinessRoundProjectBuyerEvaluations)
                .HasForeignKey(t => t.BuyerAttendeeOrganizationId);

            this.HasRequired(t => t.SellerUser)
                .WithMany()
                .HasForeignKey(d => d.SellerUserId);

            this.HasRequired(t => t.BuyerEvaluationUser)
                .WithMany()
                .HasForeignKey(d => d.BuyerEvaluationUserId);

            this.HasRequired(t => t.ProjectEvaluationStatus)
                .WithMany()
                .HasForeignKey(d => d.ProjectEvaluationStatusId);

            this.HasOptional(t => t.ProjectEvaluationRefuseReason)
                .WithMany()
                .HasForeignKey(d => d.ProjectEvaluationRefuseReasonId);
        }
    }
}