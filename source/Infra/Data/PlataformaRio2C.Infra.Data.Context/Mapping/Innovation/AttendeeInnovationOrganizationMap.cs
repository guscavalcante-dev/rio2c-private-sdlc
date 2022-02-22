// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>AttendeeInnovationOrganizationMap</summary>
    public class AttendeeInnovationOrganizationMap : EntityTypeConfiguration<AttendeeInnovationOrganization>
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationMap"/> class.</summary>
        public AttendeeInnovationOrganizationMap()
        {
            this.ToTable("AttendeeInnovationOrganizations");

            this.Property(t => t.BusinessDefinition)
                .HasMaxLength(AttendeeInnovationOrganization.BusinessDefinitionMaxLenght);

            this.Property(t => t.BusinessDifferentials)
                .HasMaxLength(AttendeeInnovationOrganization.BusinessDifferentialsMaxLenght);

            this.Property(t => t.BusinessEconomicModel)
                .HasMaxLength(AttendeeInnovationOrganization.BusinessEconomicModelMaxLenght);

            this.Property(t => t.BusinessFocus)
                .HasMaxLength(AttendeeInnovationOrganization.BusinessFocusMaxLenght);

            this.Property(t => t.BusinessOperationalModel)
                .HasMaxLength(AttendeeInnovationOrganization.BusinessOperationalModelMaxLenght);

            this.Property(t => t.BusinessStage)
                .HasMaxLength(AttendeeInnovationOrganization.BusinessStageMaxLenght);

            this.Property(t => t.MarketSize)
                .HasMaxLength(AttendeeInnovationOrganization.MarketSizeMaxLenght);

            this.Property(t => t.VideoUrl)
                .HasMaxLength(AttendeeInnovationOrganization.VideoUrlMaxLenght);


            // Relationships
            this.HasRequired(t => t.Edition)
                .WithMany()
                .HasForeignKey(d => d.EditionId);

            this.HasRequired(t => t.InnovationOrganization)
                   .WithMany(io => io.AttendeeInnovationOrganizations)
                   .HasForeignKey(d => d.InnovationOrganizationId);
        }
    }
}