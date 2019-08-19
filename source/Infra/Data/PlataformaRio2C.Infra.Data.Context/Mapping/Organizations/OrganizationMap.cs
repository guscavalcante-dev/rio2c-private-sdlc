// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
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
                .HasMaxLength(Holding.NameMaxLength)
                .IsRequired();

            // Relationships
            this.HasRequired(t => t.Holding)
                .WithMany(e => e.Organizations)
                .HasForeignKey(d => d.HoldingId);

            this.HasRequired(t => t.Updater)
                .WithMany(e => e.UpdatedOrganizations)
                .HasForeignKey(d => d.UpdateUserId);

            this.HasMany(t => t.AttendeeOrganizations)
                .WithRequired(e => e.Organization)
                .HasForeignKey(e => e.OrganizationId);

            this.HasMany(t => t.Descriptions)
                .WithRequired(e => e.Organization)
                .HasForeignKey(e => e.OrganizationId);
        }
    }
}