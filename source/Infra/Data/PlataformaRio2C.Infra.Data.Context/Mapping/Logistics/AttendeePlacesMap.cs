// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="AttendeePlacesMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeePlacesMap</summary>
    public class AttendeePlacesMap : EntityTypeConfiguration<AttendeePlace>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeePlacesMap"/> class.</summary>
        public AttendeePlacesMap()
        {
            this.ToTable("AttendeePlaces");

            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(d => d.EditionId);

            this.HasRequired(t => t.Place)
                .WithMany(e => e.AttendeePlaces)
                .HasForeignKey(d => d.PlaceId);
        }
    }
}