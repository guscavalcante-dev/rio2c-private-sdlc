// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 02-02-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-02-2022
// ***********************************************************************
// <copyright file="CartoonProjectCreatorMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>CartoonProjectCreatorMap</summary>
    public class CartoonProjectCreatorMap : EntityTypeConfiguration<CartoonProjectCreator>
    {
        /// <summary>Initializes a new instance of the <see cref="CartoonProjectCreatorMap"/> class.</summary>
        public CartoonProjectCreatorMap()
        {
            this.ToTable("CartoonProjectCreators");

            this.Property(t => t.FirstName)
                .HasMaxLength(CartoonProjectCreator.FirstNameMaxLength);

            this.Property(t => t.LastName)
                .HasMaxLength(CartoonProjectCreator.LastNameMaxLength);

            this.Property(t => t.Document)
                .HasMaxLength(CartoonProjectCreator.DocumentMaxLength);

            this.Property(t => t.Email)
                .HasMaxLength(CartoonProjectCreator.EmailMaxLength);

            this.Property(t => t.CellPhone)
                .HasMaxLength(CartoonProjectCreator.CellPhoneMaxLength);

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(CartoonProjectCreator.PhoneNumberMaxLength);

            this.Property(t => t.MiniBio)
                 .HasMaxLength(CartoonProjectCreator.MiniBioMaxLength);

            this.HasRequired(t => t.CartoonProject)
                .WithMany(e => e.CartoonProjectCreators)
                .HasForeignKey(d => d.CartoonProjectId);
        }
    }
}