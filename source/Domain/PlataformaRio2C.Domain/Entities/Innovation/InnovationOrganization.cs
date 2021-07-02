// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-29-2021
// ***********************************************************************
// <copyright file="InnovationOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class InnovationOrganization.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class InnovationOrganization : Entity
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
        public DateTimeOffset? PresentationUploadDate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganization"/> class.
        /// </summary>
        public InnovationOrganization()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganization"/> class.
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
        public InnovationOrganization(
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
            int userId)
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
            this.PresentationUploadDate = null; //TODO: Save "DateTime.Now()" when "InnovationOrganizationApiDto.PresentationFile" has value!

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        #region Valitations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool IsValid()
        {
            return true;
        }

        #endregion
    }
}
