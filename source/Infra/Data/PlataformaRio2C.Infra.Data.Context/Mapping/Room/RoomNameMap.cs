// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="RoomNameMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>RoomNameMap</summary>
    public class RoomNameMap : EntityTypeConfiguration<RoomName>
    {
        /// <summary>Initializes a new instance of the <see cref="RoomNameMap"/> class.</summary>
        public RoomNameMap()
        {
            this.ToTable("RoomNames");

            this.Property(p => p.Value)
                .HasMaxLength(RoomName.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Language)
                .WithMany()
                .HasForeignKey(d => d.LanguageId);

            this.HasRequired(t => t.Room)
                .WithMany(e => e.RoomNames)
                .HasForeignKey(d => d.RoomId);
        }
    }
}