// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="NegotiationRoomConfigMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>NegotiationRoomConfigMap</summary>
    public class NegotiationRoomConfigMap : EntityTypeConfiguration<NegotiationRoomConfig>
    {
        /// <summary>Initializes a new instance of the <see cref="NegotiationRoomConfigMap"/> class.</summary>
        public NegotiationRoomConfigMap()
        {
            this.ToTable("NegotiationRoomConfigs");

            // Relationships
            this.HasRequired(t => t.Room)
                .WithMany()
                .HasForeignKey(t => t.RoomId);

            this.HasRequired(t => t.NegotiationConfig)
                .WithMany(e => e.NegotiationRoomConfigs)
                .HasForeignKey(t => t.NegotiationConfigId);
        }
    }
}