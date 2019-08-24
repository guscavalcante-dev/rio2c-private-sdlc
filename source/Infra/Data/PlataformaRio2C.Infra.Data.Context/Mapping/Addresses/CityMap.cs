// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="CityMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Data.Entity.ModelConfiguration;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CityMap</summary>
    class CityMap : EntityTypeConfiguration<City>
    {
        /// <summary>Initializes a new instance of the <see cref="CityMap"/> class.</summary>
        public CityMap()
        {
            this.ToTable("Cities");

            this.Property(p => p.Name)
                .HasMaxLength(City.NameMaxLength);

            // Relationships
            this.HasRequired(t => t.State)
                .WithMany()
                .HasForeignKey(d => d.StateId);

            this.HasMany(t => t.Neighborhoods)
                .WithRequired(e => e.City)
                .HasForeignKey(e => e.CityId);
        }
    }
}