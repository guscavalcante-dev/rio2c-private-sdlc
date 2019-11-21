// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-21-2019
// ***********************************************************************
// <copyright file="OrganizationInterestMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>OrganizationInterestMap</summary>
    public class OrganizationInterestMap : EntityTypeConfiguration<OrganizationInterest>
    {
        /// <summary>Initializes a new instance of the <see cref="OrganizationInterestMap"/> class.</summary>
        public OrganizationInterestMap()
        {
            this.ToTable("OrganizationInterests");

            this.Property(t => t.AdditionalInfo)
                .HasMaxLength(OrganizationActivity.AdditionalInfoMaxLength);

            //Relationships
            this.HasRequired(t => t.Organization)
                .WithMany(e => e.OrganizationInterests)
                .HasForeignKey(d => d.OrganizationId);

            this.HasRequired(t => t.Interest)
               .WithMany()
               .HasForeignKey(d => d.InterestId);
        }
    }
}