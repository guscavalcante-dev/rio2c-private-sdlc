// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-16-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-16-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationFounderMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationFounderMap</summary>
    public class AttendeeInnovationOrganizationFounderMap : EntityTypeConfiguration<AttendeeInnovationOrganizationFounder>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationFounderMap"/> class.</summary>
        public AttendeeInnovationOrganizationFounderMap()
        {
            this.ToTable("AttendeeInnovationOrganizationFounders");

            this.Property(t => t.Fullname)
                .HasMaxLength(AttendeeInnovationOrganizationFounder.FullNameMaxLenght);

            this.Property(t => t.Curriculum)
                .HasMaxLength(AttendeeInnovationOrganizationFounder.CurriculumMaxLenght);

            // Relationships
            this.HasRequired(t => t.AttendeeInnovationOrganization)
                .WithMany(aio => aio.AttendeeInnovationOrganizationFounders)
                .HasForeignKey(d => d.AttendeeInnovationOrganizationId);

            this.HasRequired(t => t.WorkDedication)
                .WithMany()
                .HasForeignKey(d => d.WorkDedicationId);
        }
    }
}