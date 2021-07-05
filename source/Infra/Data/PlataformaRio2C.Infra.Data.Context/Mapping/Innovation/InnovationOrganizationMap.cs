// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationMap.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlataformaRio2C.Infra.Data.Context.Mapping
{
    /// <summary>InnovationOrganizationMap</summary>
    public class InnovationOrganizationMap : EntityTypeConfiguration<InnovationOrganization>
    {
        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationMap"/> class.</summary>
        public InnovationOrganizationMap()
        {
            this.ToTable("InnovationOrganizations");

            this.Property(t => t.Name)
               .HasMaxLength(InnovationOrganization.NameMaxLength);

            this.Property(t => t.Document)
               .HasMaxLength(InnovationOrganization.DocumentMaxLength);

            this.Property(t => t.ServiceName)
               .HasMaxLength(InnovationOrganization.ServiceNameMaxLength);

            this.Property(t => t.FoundersNames)
               .HasMaxLength(InnovationOrganization.FoundersNamesMaxLength);

            this.Property(t => t.Description)
               .HasMaxLength(InnovationOrganization.DescriptionMaxLength);

            this.Property(t => t.Curriculum)
               .HasMaxLength(InnovationOrganization.CurriculumMaxLength);

            this.Property(t => t.BusinessDefinition)
               .HasMaxLength(InnovationOrganization.BusinessDefinitionMaxLength);

            this.Property(t => t.Website)
               .HasMaxLength(InnovationOrganization.WebsiteMaxLength);

            this.Property(t => t.BusinessFocus)
               .HasMaxLength(InnovationOrganization.BusinessFocusMaxLength);

            this.Property(t => t.MarketSize)
               .HasMaxLength(InnovationOrganization.MarketSizeMaxLength);

            this.Property(t => t.BusinessEconomicModel)
               .HasMaxLength(InnovationOrganization.BusinessEconomicModelMaxLength);

            this.Property(t => t.BusinessOperationalModel)
               .HasMaxLength(InnovationOrganization.BusinessOperationalModelMaxLength);

            this.Property(t => t.BusinessDifferentials)
               .HasMaxLength(InnovationOrganization.BusinessDifferentialsMaxLength);

            this.Property(t => t.CompetingCompanies)
               .HasMaxLength(InnovationOrganization.CompetingCompaniesMaxLength);

            this.Property(t => t.BusinessStage)
               .HasMaxLength(InnovationOrganization.BusinessStageMaxLength);

            // Relationships
            this.HasRequired(t => t.WorkDedication)
                .WithMany()
                .HasForeignKey(d => d.WorkDedicationId);
        }
    }
}