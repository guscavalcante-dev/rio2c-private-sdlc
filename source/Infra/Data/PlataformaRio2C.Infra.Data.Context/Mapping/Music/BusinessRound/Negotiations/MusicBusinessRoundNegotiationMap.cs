// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-07-2020
// ***********************************************************************
// <copyright file="MusicBusinessRoundNegotiationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>MusicBusinessRoundNegotiationMap</summary>
    public class MusicBusinessRoundNegotiationMap : EntityTypeConfiguration<MusicBusinessRoundNegotiation>
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationMap"/> class.</summary>
        public MusicBusinessRoundNegotiationMap()
        {
            this.ToTable("MusicBusinessRoundNegotiations");

            // Relationships
            this.HasRequired(t => t.MusicBusinessRoundProjectBuyerEvaluation)
                .WithMany(e => e.MusicBusinessRoundNegotiations)
                .HasForeignKey(t => t.MusicBusinessRoundProjectBuyerEvaluationId);

            this.HasRequired(t => t.Room)
                .WithMany()
                .HasForeignKey(t => t.RoomId);

            this.HasRequired(t => t.Updater)
               .WithMany(e => e.UpdatedMusicBusinessRoundNegotiations)
               .HasForeignKey(d => d.UpdateUserId);
        }
    }
}