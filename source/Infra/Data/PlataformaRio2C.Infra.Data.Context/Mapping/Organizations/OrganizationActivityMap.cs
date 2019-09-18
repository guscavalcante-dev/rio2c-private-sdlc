// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-18-2019
// ***********************************************************************
// <copyright file="OrganizationActivityMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>OrganizationActivityMap</summary>
    public class OrganizationActivityMap : EntityTypeConfiguration<OrganizationActivity>
    {
        /// <summary>Initializes a new instance of the <see cref="OrganizationActivityMap"/> class.</summary>
        public OrganizationActivityMap()
        {
            this.ToTable("OrganizationActivities");

            this.Property(t => t.AdditionalInfo)
                .HasMaxLength(OrganizationActivity.AdditionalInfoMaxLength);

            //Relationships
            this.HasRequired(t => t.Organization)
                .WithMany(e => e.OrganizationActivities)
                .HasForeignKey(d => d.OrganizationId);

            this.HasRequired(t => t.Activity)
               .WithMany()
               .HasForeignKey(d => d.ActivityId);
        }
    }
}