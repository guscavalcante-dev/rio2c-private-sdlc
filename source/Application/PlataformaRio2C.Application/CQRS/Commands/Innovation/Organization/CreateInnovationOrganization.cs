// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 28-06-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 28-06-2021
// ***********************************************************************
// <copyright file="CreateInnovationOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateInnovationOrganization</summary>
    public class CreateInnovationOrganization : BaseCommand
    {
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string ServiceName { get; private set; }
        public string FoundersNames { get; private set; }
        public DateTime FoundationDate { get; private set; }
        public decimal AccumulatedRevenue { get; private set; }
        public string Description { get; private set; }
        public string Curriculum { get; private set; }
        public int WorkDedicationId { get; private set; }
        public string BusinessDefinition { get; private set; }
        public string Website { get; private set; }
        public string BusinessFocus { get; private set; }
        public string MarketSize { get; private set; }
        public string BusinessEconomicModel { get; private set; }
        public string BusinessOperationalModel { get; private set; }
        public string BusinessDifferentials { get; private set; }
        public string CompetingCompanies { get; private set; }
        public string BusinessStage { get; private set; }
        public string ResponsibleName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string CellPhone { get; private set; }

        public List<InnovationOptionApiDto> CompanyExperiences { get; set; }
        public List<InnovationOptionApiDto> ProductsOrServices { get; set; }
        public List<InnovationOptionApiDto> TechnologyExperiences { get; set; }
        public List<InnovationOptionApiDto> CompanyObjectives { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationOrganization"/> class.
        /// </summary>
        /// <param name="innovationOrganizationApiDto">The innovation organization API dto.</param>
        public CreateInnovationOrganization(InnovationOrganizationApiDto innovationOrganizationApiDto)
        {
            this.Name = innovationOrganizationApiDto.Name;
            this.Document = innovationOrganizationApiDto.Document;
            this.ServiceName = innovationOrganizationApiDto.ServiceName;
            this.FoundersNames = innovationOrganizationApiDto.FoundersNames.ToString(";");
            this.FoundationDate = innovationOrganizationApiDto.FoundationDate;
            this.AccumulatedRevenue = innovationOrganizationApiDto.AccumulatedRevenue;
            this.Description = innovationOrganizationApiDto.Description;
            this.Curriculum = innovationOrganizationApiDto.Curriculum;
            this.WorkDedicationId = innovationOrganizationApiDto.WorkDedicationId;
            this.BusinessDefinition = innovationOrganizationApiDto.BusinessDefinition;
            this.Website = innovationOrganizationApiDto.Website;
            this.BusinessFocus = innovationOrganizationApiDto.BusinessFocus;
            this.MarketSize = innovationOrganizationApiDto.MarketSize;
            this.BusinessEconomicModel = innovationOrganizationApiDto.BusinessEconomicModel;
            this.BusinessOperationalModel = innovationOrganizationApiDto.BusinessOperationalModel;
            this.BusinessDifferentials = innovationOrganizationApiDto.BusinessDifferentials;
            this.CompetingCompanies = innovationOrganizationApiDto.CompetingCompanies.ToString(";");
            this.BusinessStage = innovationOrganizationApiDto.BusinessStage;
            this.ResponsibleName = innovationOrganizationApiDto.ResponsibleName;
            this.Email = innovationOrganizationApiDto.Email;
            this.PhoneNumber = innovationOrganizationApiDto.PhoneNumber;
            this.CellPhone = innovationOrganizationApiDto.CellPhone;
            this.CompanyExperiences = innovationOrganizationApiDto.CompanyExperiences;
            this.ProductsOrServices = innovationOrganizationApiDto.ProductsOrServices;
            this.TechnologyExperiences = innovationOrganizationApiDto.TechnologyExperiences;
            this.CompanyObjectives = innovationOrganizationApiDto.CompanyObjectives;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInnovationOrganization"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="document">The document.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="foundersNames">The founders names.</param>
        /// <param name="foundationDate">The foundation date.</param>
        /// <param name="accumulatedRevenue">The accumulated revenue.</param>
        /// <param name="description">The description.</param>
        /// <param name="curriculum">The curriculum.</param>
        /// <param name="workDedicationId">The work dedication identifier.</param>
        /// <param name="businessDefinition">The business definition.</param>
        /// <param name="website">The website.</param>
        /// <param name="businessFocus">The business focus.</param>
        /// <param name="marketSize">Size of the market.</param>
        /// <param name="businessEconomicModel">The business economic model.</param>
        /// <param name="businessOperationalModel">The business operational model.</param>
        /// <param name="businessDifferentials">The business differentials.</param>
        /// <param name="competingCompanies">The competing companies.</param>
        /// <param name="businessStage">The business stage.</param>
        /// <param name="presentationUploadDate">The presentation upload date.</param>
        public CreateInnovationOrganization(
            string name,
            string document,
            string serviceName,
            string foundersNames,
            DateTime foundationDate,
            decimal accumulatedRevenue,
            string description,
            string curriculum,
            int workDedicationId,
            string businessDefinition,
            string website,
            string businessFocus,
            string marketSize,
            string businessEconomicModel,
            string businessOperationalModel,
            string businessDifferentials,
            string competingCompanies,
            string businessStage,
            string responsibleName,
            string email,
            string phoneNumber,
            string cellPhone,
            List<InnovationOptionApiDto> companyExperiences,
            List<InnovationOptionApiDto> productsOrServices,
            List<InnovationOptionApiDto> technologyExperiences,
            List<InnovationOptionApiDto> companyObjectives)
        {
            this.Name = name;
            this.Document = document;
            this.ServiceName = serviceName;
            this.FoundersNames = foundersNames;
            this.FoundationDate = foundationDate;
            this.AccumulatedRevenue = accumulatedRevenue;
            this.Description = description;
            this.Curriculum = curriculum;
            this.WorkDedicationId = workDedicationId;
            this.BusinessDefinition = businessDefinition;
            this.Website = website;
            this.BusinessFocus = businessFocus;
            this.MarketSize = marketSize;
            this.BusinessEconomicModel = businessEconomicModel;
            this.BusinessOperationalModel = businessOperationalModel;
            this.BusinessDifferentials = businessDifferentials;
            this.CompetingCompanies = competingCompanies;
            this.BusinessStage = businessStage;
            this.ResponsibleName = responsibleName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.CellPhone = cellPhone;
            this.CompanyExperiences = companyExperiences;
            this.ProductsOrServices = productsOrServices;
            this.TechnologyExperiences = technologyExperiences;
            this.CompanyObjectives = companyObjectives;
        }
    }
}