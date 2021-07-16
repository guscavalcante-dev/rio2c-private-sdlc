// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-16-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-16-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationExperienceMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationExperienceMap</summary>
    public class AttendeeInnovationOrganizationExperienceMap : EntityTypeConfiguration<AttendeeInnovationOrganizationExperience>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationExperienceMap"/> class.</summary>
        public AttendeeInnovationOrganizationExperienceMap()
        {
            this.ToTable("AttendeeInnovationOrganizationExperiences");

            // Relationships
            this.HasRequired(t => t.AttendeeInnovationOrganization)
                .WithMany(aio => aio.AttendeeInnovationOrganizationExperiences)
                .HasForeignKey(d => d.AttendeeInnovationOrganizationId);

            this.HasRequired(t => t.InnovationOrganizationExperienceOption)
                .WithMany()
                .HasForeignKey(d => d.InnovationOrganizationExperienceOptionId);
        }
    }
}