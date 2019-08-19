// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-19-2019
// ***********************************************************************
// <copyright file="OrganizationTypeMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>OrganizationTypeMap</summary>
    public class OrganizationTypeMap : EntityTypeConfiguration<OrganizationType>
    {
        /// <summary>Initializes a new instance of the <see cref="OrganizationMap"/> class.</summary>
        public OrganizationTypeMap()
        {
            this.ToTable("OrganizationTypes");

            this.Property(t => t.Name)
                .HasMaxLength(OrganizationType.NameMaxLength)
                .IsRequired();

            // Relationships
            this.HasRequired(t => t.RelatedProjectType)
                .WithMany(e => e.OrganizationTypes)
                .HasForeignKey(d => d.RelatedProjectTypeId);

            this.HasMany(t => t.AttendeeOrganizationTypes)
                .WithRequired(e => e.OrganizationType)
                .HasForeignKey(e => e.OrganizationTypeId);

            //this.HasOptional(t => t.Image)
            //    .WithMany()
            //    .HasForeignKey(d => d.ImageId);
        }
    }
}