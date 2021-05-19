// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
// ***********************************************************************
// <copyright file="NegotiationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>NegotiationMap</summary>
    public class NegotiationMap : EntityTypeConfiguration<Negotiation>
    {
        /// <summary>Initializes a new instance of the <see cref="NegotiationMap"/> class.</summary>
        public NegotiationMap()
        {
            this.ToTable("Negotiations");

            // Relationships
            this.HasRequired(t => t.ProjectBuyerEvaluation)
                .WithMany(e => e.Negotiations)
                .HasForeignKey(t => t.ProjectBuyerEvaluationId);

            this.HasRequired(t => t.Room)
                .WithMany()
                .HasForeignKey(t => t.RoomId);

            this.HasRequired(t => t.Updater)
               .WithMany(e => e.UpdatedNegotiations)
               .HasForeignKey(d => d.UpdateUserId);
        }
    }
}