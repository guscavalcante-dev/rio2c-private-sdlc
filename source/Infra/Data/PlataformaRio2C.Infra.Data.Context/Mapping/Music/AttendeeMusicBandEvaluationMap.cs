// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 03-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-30-2021
// ***********************************************************************
// <copyright file="AttendeeMusicBandEvaluationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeMusicBandEvaluationMap</summary>
    public class AttendeeMusicBandEvaluationMap : EntityTypeConfiguration<AttendeeMusicBandEvaluation>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBandMap"/> class.</summary>
        public AttendeeMusicBandEvaluationMap()
        {
            this.ToTable("AttendeeMusicBandEvaluations");

            // Relationships
            this.HasRequired(t => t.AttendeeMusicBand)
                .WithMany()
                .HasForeignKey(d => d.AttendeeMusicBandId);

            this.HasRequired(t => t.EvaluatorUser)
                .WithMany()
                .HasForeignKey(d => d.EvaluatorUserId);

            this.HasOptional(t => t.AttendeeMusicBand)
                .WithMany(e => e.AttendeeMusicBandEvaluations)
                .HasForeignKey(t => t.AttendeeMusicBandId);
        }
    }
}