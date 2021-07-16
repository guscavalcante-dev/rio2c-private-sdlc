// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-16-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-16-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationTechnologyMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationTechnologyMap</summary>
    public class AttendeeInnovationOrganizationTechnologyMap : EntityTypeConfiguration<AttendeeInnovationOrganizationTechnology>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTechnologyMap"/> class.</summary>
        public AttendeeInnovationOrganizationTechnologyMap()
        {
            this.ToTable("AttendeeInnovationOrganizationTechnologies");

            // Relationships
            this.HasRequired(t => t.AttendeeInnovationOrganization)
                .WithMany(aio => aio.AttendeeInnovationOrganizationTechnologies)
                .HasForeignKey(d => d.AttendeeInnovationOrganizationId);

            this.HasRequired(t => t.InnovationOrganizationTechnologyOption)
                .WithMany()
                .HasForeignKey(d => d.InnovationOrganizationTechnologyOptionId);
        }
    }
}