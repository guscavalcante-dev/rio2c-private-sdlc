// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="RoomMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>RoomMap</summary>
    public class RoomMap : EntityTypeConfiguration<Room>
    {
        public RoomMap()
        {
            this.ToTable("Rooms");

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(t => t.EditionId);
        }
    }
}