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

            Property(u => u.Number)
              .HasMaxLength(Address.NumberMaxLength);

            Property(u => u.Complement)
                .HasMaxLength(Address.ComplementMaxLength);

            // Relationships
            this.HasRequired(t => t.Street)
                .WithMany()
                .HasForeignKey(d => d.StreetId);

            //this.HasMany<Country>(t => t.Countries)
            //   //.WithRequired( s=> s.Address)
            //   ;

            //this.HasMany<State>(t => t.States)
            //   //.WithRequired(s => s.Address)
            //   ;

            //this.HasMany<City>(t => t.Cities)
            //   //.WithRequired(s => s.Address)
            //   ;
        }
    }
}
