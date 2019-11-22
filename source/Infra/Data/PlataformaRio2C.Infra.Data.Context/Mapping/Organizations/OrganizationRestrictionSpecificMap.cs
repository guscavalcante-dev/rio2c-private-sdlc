// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 09-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-22-2019
// ***********************************************************************
// <copyright file="OrganizationRestrictionSpecificMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>OrganizationRestrictionSpecificMap</summary>
    public class OrganizationRestrictionSpecificMap : EntityTypeConfiguration<OrganizationRestrictionSpecific>
    {
        /// <summary>Initializes a new instance of the <see cref="OrganizationRestrictionSpecificMap"/> class.</summary>
        public OrganizationRestrictionSpecificMap()
        {
            this.ToTable("OrganizationRestrictionSpecifics");

            this.Property(p => p.Value)
                .HasMaxLength(OrganizationDescription.ValueMaxLength);

            //Relationships
            this.HasRequired(t => t.Organization)
                .WithMany(e => e.OrganizationRestrictionSpecifics)
                .HasForeignKey(d => d.OrganizationId);

            this.HasRequired(t => t.Language)
               .WithMany()
               .HasForeignKey(d => d.LanguageId);
        }
    }
}