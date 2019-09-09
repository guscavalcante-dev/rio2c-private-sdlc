// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
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

            this.Property(t => t.SocialMedia)
                .HasMaxLength(Organization.SocialMediaMaxLength);

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

            this.HasMany(t => t.AttendeeOrganizations)
                .WithRequired(e => e.Organization)
                .HasForeignKey(e => e.OrganizationId);

            this.HasMany(t => t.Descriptions)
                .WithRequired(e => e.Organization)
                .HasForeignKey(e => e.OrganizationId);

            this.HasMany(t => t.OrganizationActivities)
                .WithRequired(e => e.Organization)
                .HasForeignKey(e => e.OrganizationId);

            this.HasMany(t => t.OrganizationTargetAudiences)
                .WithRequired(e => e.Organization)
                .HasForeignKey(e => e.OrganizationId);

            this.HasMany(t => t.OrganizationInterests)
                .WithRequired(e => e.Organization)
                .HasForeignKey(e => e.OrganizationId);
        }
    }
}