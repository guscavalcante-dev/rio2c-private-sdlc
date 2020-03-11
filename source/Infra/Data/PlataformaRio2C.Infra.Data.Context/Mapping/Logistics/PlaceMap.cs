// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="PlaceMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>PlaceMap</summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{PlataformaRio2C.Domain.Entities.Place}" />
    public class PlaceMap : EntityTypeConfiguration<Place>
    {
        /// <summary>Initializes a new instance of the <see cref="PlaceMap"/> class.</summary>
        public PlaceMap()
        {
            this.ToTable("Places");

            this.Property(p => p.Name)
                .HasMaxLength(Place.NameMaxLength);

            this.Property(p => p.Website)
                .HasMaxLength(Place.WebsiteMaxLength);

            this.Property(p => p.AdditionalInfo)
                .HasMaxLength(Place.AdditionalInfoMaxLength);

            // Relationships
            this.HasOptional(e => e.Address)
                .WithMany()
                .HasForeignKey(e => e.AddressId);
        }
    }
}