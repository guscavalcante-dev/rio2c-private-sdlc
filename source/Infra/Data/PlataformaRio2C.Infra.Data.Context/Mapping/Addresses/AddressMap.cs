// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="CityMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary></summary>
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        /// <summary>Initializes a new instance of the <see cref="AddressMap"/> class.</summary>
        public AddressMap()
        {
            this.ToTable("Addresses");

            Property(u => u.Address1)
              .HasMaxLength(Address.Address1MaxLength);

            Property(u => u.Address2)
                .HasMaxLength(Address.Address2MaxLength);

            Property(u => u.ZipCode)
                .HasMaxLength(Address.ZipCodeMaxLength);

            // Relationships
            this.HasRequired(t => t.City)
                .WithMany()
                .HasForeignKey(d => d.CityId);

            this.HasMany(t => t.Organizations)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.AddressId);
        }
    }
}