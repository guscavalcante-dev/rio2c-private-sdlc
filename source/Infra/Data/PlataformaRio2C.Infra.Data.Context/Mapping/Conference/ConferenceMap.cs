// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="ConferenceMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>ConferenceMap</summary>
    public class ConferenceMap : EntityTypeConfiguration<Conference>
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceMap"/> class.</summary>
        public ConferenceMap()
        {
            this.ToTable("Conferences");

            //Relationships 
            this.HasRequired(t => t.EditionEvent)
                .WithMany(d => d.Conferences)
                .HasForeignKey(t => t.EditionEventId);

            this.HasOptional(t => t.Room)
              .WithMany(d => d.Conferences)
              .HasForeignKey(t => t.RoomId);
        }
    }
}