// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 02-02-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-02-2022
// ***********************************************************************
// <copyright file="CartoonProjectOrganizationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CartoonProjectOrganizationMap</summary>
    public class CartoonProjectOrganizationMap : EntityTypeConfiguration<CartoonProjectOrganization>
    {
        /// <summary>Initializes a new instance of the <see cref="CartoonProjectOrganizationMap"/> class.</summary>
        public CartoonProjectOrganizationMap()
        {
            this.ToTable("CartoonProjectOrganizations");

            this.Property(t => t.Name)
                .HasMaxLength(CartoonProjectOrganization.NameMaxLength);

            this.Property(t => t.TradeName)
                .HasMaxLength(CartoonProjectOrganization.TradeNameMaxLength);

            this.Property(t => t.Document)
                .HasMaxLength(CartoonProjectOrganization.DocumentMaxLength);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(CartoonProjectOrganization.PhoneNumberMaxLength);

            this.Property(t => t.ReelUrl)
                .HasMaxLength(CartoonProjectOrganization.ReelUrlMaxLength);

            this.HasRequired(t => t.CartoonProject)
                .WithMany(x => x.CartoonProjectOrganizations)
                .HasForeignKey(d => d.CartoonProjectId);

            this.HasOptional(t => t.Address)
                .WithMany()
                .HasForeignKey(d => d.AddressId);
        }
    }
}