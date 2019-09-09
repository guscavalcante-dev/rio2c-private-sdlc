// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 09-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="OrganizationTargetAudienceMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>OrganizationActivityMap</summary>
    public class OrganizationTargetAudienceMap : EntityTypeConfiguration<OrganizationTargetAudience>
    {
        /// <summary>Initializes a new instance of the <see cref="OrganizationTargetAudienceMap"/> class.</summary>
        public OrganizationTargetAudienceMap()
        {
            this.ToTable("OrganizationTargetAudiences");

            //Relationships
            this.HasRequired(t => t.Organization)
                .WithMany(e => e.OrganizationTargetAudiences)
                .HasForeignKey(d => d.OrganizationId);

            this.HasRequired(t => t.TargetAudience)
               .WithMany()
               .HasForeignKey(d => d.TargetAudienceId);
        }
    }
}