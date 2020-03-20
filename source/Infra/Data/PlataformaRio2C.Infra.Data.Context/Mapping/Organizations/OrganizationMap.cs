// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
// ***********************************************************************
// <copyright file="OrganizationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>OrganizationMap</summary>
    public class OrganizationMap : EntityTypeConfiguration<Organization>
    {
        /// <summary>Initializes a new instance of the <see cref="OrganizationMap"/> class.</summary>
        public OrganizationMap()
        {
            this.ToTable("Organizations");

            this.Property(t => t.Name)
                .HasMaxLength(Organization.NameMaxLength)
                .IsRequired();

            this.Property(t => t.CompanyName)
                .HasMaxLength(Organization.CompanyNameMaxLength);

            this.Property(t => t.TradeName)
                .HasMaxLength(Organization.TradeNameMaxLength);

            this.Property(t => t.Website)
                .HasMaxLength(Organization.WebsiteMaxLength);

            this.Property(t => t.Linkedin)
                .HasMaxLength(Organization.LinkedinMaxLength);

            this.Property(t => t.Twitter)
                .HasMaxLength(Organization.TwitterMaxLength);

            this.Property(t => t.Instagram)
                .HasMaxLength(Organization.InstagramMaxLength);

            this.Property(t => t.Youtube)
                .HasMaxLength(Organization.YoutubeMaxLength);

            // Relationships
            this.HasOptional(t => t.Holding)
                .WithMany(e => e.Organizations)
                .HasForeignKey(d => d.HoldingId);

            this.HasRequired(t => t.Updater)
                .WithMany(e => e.UpdatedOrganizations)
                .HasForeignKey(d => d.UpdateUserId);

            this.HasOptional(t => t.Address)
                .WithMany(e => e.Organizations)
                .HasForeignKey(d => d.AddressId);
        }
    }
}