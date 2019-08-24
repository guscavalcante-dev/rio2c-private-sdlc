// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 08-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="NeighborhoodMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Data.Entity.ModelConfiguration;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>NeighborhoodMap</summary>
    class NeighborhoodMap : EntityTypeConfiguration<Neighborhood>
    {
        /// <summary>Initializes a new instance of the <see cref="NeighborhoodMap"/> class.</summary>
        public NeighborhoodMap()
        {
            this.ToTable("Neighborhoods");

            this.Property(p => p.Name)
                .HasMaxLength(Neighborhood.NameMaxLength);

            // Relationships
            this.HasRequired(t => t.City)
                .WithMany()
                .HasForeignKey(d => d.CityId);

            this.HasMany(t => t.Streets)
                .WithRequired(e => e.Neighborhood)
                .HasForeignKey(e => e.NeighborhoodId);
        }
    }
}