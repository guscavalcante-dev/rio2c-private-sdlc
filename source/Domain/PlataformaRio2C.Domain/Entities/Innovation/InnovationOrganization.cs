// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-07-2021
// ***********************************************************************
// <copyright file="InnovationOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{

    /// <summary>
    /// Class InnovationOrganizations.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.AggregateRoot" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.AggregateRoot" />
    public class InnovationOrganization : AggregateRoot
    {
        public static readonly int NameMaxLength = 100;
        public static readonly int DocumentMaxLength = 50;
        public static readonly int ServiceNameMaxLength = 150;
        public static readonly int FoundersNamesMaxLength = 1000;
        public static readonly int DescriptionMaxLength = 600;
        public static readonly int CurriculumMaxLength = 600;
        public static readonly int BusinessDefinitionMaxLength = 300;
        public static readonly int WebsiteMaxLength = 150;
        public static readonly int BusinessFocusMaxLength = 300;
        public static readonly int MarketSizeMaxLength = 300;
        public static readonly int BusinessEconomicModelMaxLength = 300;
        public static readonly int BusinessOperationalModelMaxLength = 300;
        public static readonly int BusinessDifferentialsMaxLength = 300;
        public static readonly int CompetingCompaniesMaxLength = 300;
        public static readonly int BusinessStageMaxLength = 300;

        public string Name { get; private set; }
        public string Document { get; private set; }
        public string ServiceName { get; private set; }
        public DateTime FoundationDate { get; private set; }
        public string Description { get; private set; }
        public string Website { get; private set; }
        public DateTimeOffset? ImageUploadDate { get; private set; }

        public virtual ICollection<AttendeeInnovationOrganization> AttendeeInnovationOrganizations { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganization"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="name">The name.</param>
        /// <param name="document">The document.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="foundationDate">The foundation date.</param>
        /// <param name="description">The description.</param>
        /// <param name="website">The website.</param>
        /// <param name="accumulatedRevenue">The accumulated revenue.</param>
        /// <param name="businessDefinition">The business definition.</param>
        /// <param name="businessFocus">The business focus.</param>
        /// <param name="marketSize">Size of the market.</param>
        /// <param name="businessEconomicModel">The business economic model.</param>
        /// <param name="businessOperationalModel">The business operational model.</param>
        /// <param name="businessDifferentials">The business differentials.</param>
        /// <param name="businessStage">The business stage.</param>
        /// <param name="isPresentationUploaded">if set to <c>true</c> [is presentation uploaded].</param>
        /// <param name="innovationOrganizationExperienceOptions">The innovation organization experience options.</param>
        /// <param name="innovationOrganizationObjectivesOptions">The innovation organization objectives options.</param>
        /// <param name="innovationOrganizationTechnologyOptions">The innovation organization technology options.</param>
        /// <param name="innovationOrganizationTrackOptions">The innovation organization track options.</param>
        /// <param name="attendeeInnovationOrganizationFounderApiDtos">The attendee innovation organization founder API dtos.</param>
        /// <param name="attendeeInnovationOrganizationCompetitorApiDtos">The attendee innovation organization competitor API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        public InnovationOrganization(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            string name,
            string document,
            string serviceName,
            DateTime foundationDate,
            string description,
            string website,
            decimal accumulatedRevenue,
            string businessDefinition,
            string businessFocus,
            string marketSize,
            string businessEconomicModel,
            string businessOperationalModel,
            string businessDifferentials,
            string businessStage,
            bool isPresentationUploaded,
            List<AttendeeInnovationOrganizationFounderApiDto> attendeeInnovationOrganizationFounderApiDtos,
            List<AttendeeInnovationOrganizationCompetitorApiDto> attendeeInnovationOrganizationCompetitorApiDtos,
            List<InnovationOrganizationExperienceOptionApiDto> innovationOrganizationExperienceOptionApiDtos,
            List<InnovationOrganizationObjectivesOptionApiDto> innovationOrganizationObjectivesOptionApiDtos,
            List<InnovationOrganizationTechnologyOptionApiDto> innovationOrganizationTechnologyOptionApiDtos,
            List<InnovationOrganizationTrackOptionApiDto> innovationOrganizationTrackOptionApiDtos,
            int userId)
        {
            this.Name = name;
            this.Document = document;
            this.ServiceName = serviceName;
            this.FoundationDate = foundationDate;
            this.Description = description;
            this.Website = website;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;

            this.SynchronizeAttendeeInnovationOrganizations(
                edition,
                accumulatedRevenue,
                marketSize,
                businessDefinition,
                businessFocus,
                businessEconomicModel,
                businessDifferentials,
                businessStage,
                isPresentationUploaded,
                businessOperationalModel,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationCollaborators(
                edition,
                attendeeCollaborator,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationFounders(
                edition,
                attendeeInnovationOrganizationFounderApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationCompetitors(
                edition,
                attendeeInnovationOrganizationCompetitorApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationExperiences(
                edition,
                innovationOrganizationExperienceOptionApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationObjectives(
                edition,
                innovationOrganizationObjectivesOptionApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationTechnologies(
                edition,
                innovationOrganizationTechnologyOptionApiDtos,
                userId);

            this.SynchronizeAttendeeInnovationOrganizationTracks(
                edition,
                innovationOrganizationTrackOptionApiDtos,
                userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganization"/> class.
        /// </summary>
        public InnovationOrganization()
        {

        }

        #region Attendee Innovation Organization

        /// <summary>
        /// Synchronizes the attendee innovation organizations.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="accumulatedRevenue">The accumulated revenue.</param>
        /// <param name="marketSize">Size of the market.</param>
        /// <param name="businessDefinition">The business definition.</param>
        /// <param name="businessFocus">The business focus.</param>
        /// <param name="businessEconomicModel">The business economic model.</param>
        /// <param name="businessDifferentials">The business differentials.</param>
        /// <param name="businessStage">The business stage.</param>
        /// <param name="isPresentationUploaded">if set to <c>true</c> [is presentation uploaded].</param>
        /// <param name="businessOperationalModel">The business operational model.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizations(
            Edition edition,
            decimal accumulatedRevenue,
            string marketSize,
            string businessDefinition,
            string businessFocus,
            string businessEconomicModel,
            string businessDifferentials,
            string businessStage,
            bool isPresentationUploaded,
            string businessOperationalModel,
            int userId)
        {
            if (edition == null)
            {
                return;
            }

            if (this.AttendeeInnovationOrganizations == null)
            {
                this.AttendeeInnovationOrganizations = new List<AttendeeInnovationOrganization>();
            }

            var attendeeInnovationOrganization = this.AttendeeInnovationOrganizations.FirstOrDefault(ao => ao.EditionId == edition.Id);
            if (attendeeInnovationOrganization != null)
            {
                attendeeInnovationOrganization.Restore(userId);
            }
            else
            {
                this.AttendeeInnovationOrganizations.Add(
                    new AttendeeInnovationOrganization(
                    edition,
                    this,
                    accumulatedRevenue,
                    marketSize,
                    businessDefinition,
                    businessFocus,
                    businessEconomicModel,
                    businessDifferentials,
                    businessStage,
                    isPresentationUploaded,
                    businessOperationalModel,
                    userId));
            }
        }

        /// <summary>
        /// Gets the attendee innovation organization by edition identifier.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public AttendeeInnovationOrganization GetAttendeeInnovationOrganizationByEditionId(int editionId)
        {
            return this.AttendeeInnovationOrganizations?.FirstOrDefault(aio => aio.Edition.Id == editionId);
        }

        #endregion

        #region Attendee Innovation Organization Collaborators

        /// <summary>
        /// Synchronizes the attendee innovation organization collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationCollaborators(
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationColaborators(
                     attendeeCollaborator,
                     userId);
        }

        #endregion

        #region Attendee Innovation Organization Founder

        /// <summary>
        /// Synchronizes the attendee innovation organization founders.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganizationFounderOptionApiDtos">The innovation organization founder option API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationFounders(
            Edition edition,
            List<AttendeeInnovationOrganizationFounderApiDto> innovationOrganizationFounderOptionApiDtos,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var innovationOrganizationFounderOptionApiDto in innovationOrganizationFounderOptionApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationFounders(
                    innovationOrganizationFounderOptionApiDto.WorkDedication,
                    innovationOrganizationFounderOptionApiDto.FullName,
                    innovationOrganizationFounderOptionApiDto.Curriculum,
                    userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Innovation Organization Competitor

        /// <summary>
        /// Synchronizes the attendee innovation organization competitors.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="name">The name.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationCompetitors(
        Edition edition,
        List<AttendeeInnovationOrganizationCompetitorApiDto> attendeeInnovationOrganizationCompetitorApiDtos,
        int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var attendeeInnovationOrganizationCompetitorApiDto in attendeeInnovationOrganizationCompetitorApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationCompetitors(attendeeInnovationOrganizationCompetitorApiDto.Name, userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Innovation Organization Experience

        /// <summary>
        /// Synchronizes the attendee innovation organization experiences.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganizationExperienceOptionApiDtos">The innovation organization experience option API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationExperiences(
            Edition edition,
            List<InnovationOrganizationExperienceOptionApiDto> innovationOrganizationExperienceOptionApiDtos,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var innovationOrganizationExperienceOptionApiDto in innovationOrganizationExperienceOptionApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationExperiences(
                    innovationOrganizationExperienceOptionApiDto.InnovationOrganizationExperienceOption,
                    innovationOrganizationExperienceOptionApiDto.AdditionalInfo,
                    userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Innovation Organization Objective

        /// <summary>
        /// Synchronizes the attendee innovation organization experiences.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganizationObjectivesOptionApiDtos">The innovation organization objectives option API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationObjectives(
            Edition edition,
            List<InnovationOrganizationObjectivesOptionApiDto> innovationOrganizationObjectivesOptionApiDtos,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var innovationOrganizationObjectivesOptionApiDto in innovationOrganizationObjectivesOptionApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationObjectives(
                    innovationOrganizationObjectivesOptionApiDto.InnovationOrganizationObjectivesOption,
                    innovationOrganizationObjectivesOptionApiDto.AdditionalInfo,
                    userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Innovation Organization Technology

        /// <summary>
        /// Synchronizes the attendee innovation organization technologies.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganizationTechnologyOptionApiDtos">The innovation organization technology option API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationTechnologies(
            Edition edition,
            List<InnovationOrganizationTechnologyOptionApiDto> innovationOrganizationTechnologyOptionApiDtos,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var innovationOrganizationTechnologyOptionApiDto in innovationOrganizationTechnologyOptionApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationTechnologies(
                    innovationOrganizationTechnologyOptionApiDto.InnovationOrganizationTechnologyOption,
                    innovationOrganizationTechnologyOptionApiDto.AdditionalInfo,
                    userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Innovation Organization Track

        /// <summary>
        /// Synchronizes the attendee innovation organization tracks.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="innovationOrganizationTrackOptionApiDtos">The innovation organization track option API dtos.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeInnovationOrganizationTracks(
            Edition edition,
            List<InnovationOrganizationTrackOptionApiDto> innovationOrganizationTrackOptionApiDtos,
            int userId)
        {
            var attendeeInnovationOrganization = this.GetAttendeeInnovationOrganizationByEditionId(edition?.Id ?? 0);

            foreach (var innovationOrganizationTrackOptionApiDto in innovationOrganizationTrackOptionApiDtos)
            {
                attendeeInnovationOrganization?.SynchronizeAttendeeInnovationOrganizationTracks(
                    innovationOrganizationTrackOptionApiDto.InnovationOrganizationTrackOption,
                    innovationOrganizationTrackOptionApiDto.AdditionalInfo,
                    userId);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Valitations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            //this.ValidateName();
            //this.ValidateDocument();
            //this.ValidateServiceName();
            //this.ValidateFoundersNames();
            //this.ValidateDescription();
            //this.ValidateCurriculum();
            //this.ValidateBusinessDefinition();
            //this.ValidateWebsite();
            //this.ValidateBusinessFocus();
            //this.ValidateMarketSize();
            //this.ValidateBusinessEconomicModel();
            //this.ValidateBusinessOperationalModel();
            //this.ValidateBusinessDifferentials();
            //this.ValidateCompetingCompanies();
            //this.ValidateBusinessStage();

            return this.ValidationResult.IsValid;
        }

        ///// <summary>
        ///// Validates the name.
        ///// </summary>
        //private void ValidateName()
        //{
        //    if (string.IsNullOrEmpty(this.Name?.Trim()))
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { nameof(Name) }));
        //    }

        //    if (this.Name?.Length > NameMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, 1), new string[] { nameof(Name) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the document.
        ///// </summary>
        //private void ValidateDocument()
        //{
        //    if (this.Document?.Length > DocumentMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, DocumentMaxLength, 1), new string[] { nameof(Document) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the name of the service.
        ///// </summary>
        //private void ValidateServiceName()
        //{
        //    if (string.IsNullOrEmpty(this.ServiceName?.Trim()))
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "ServiceName"), new string[] { nameof(ServiceName) }));
        //    }

        //    if (this.ServiceName?.Length > ServiceNameMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "ServiceName", ServiceNameMaxLength, 1), new string[] { nameof(ServiceName) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the names of the founders.
        ///// </summary>
        //private void ValidateFoundersNames()
        //{
        //    if (string.IsNullOrEmpty(this.FoundersNames?.Trim()))
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "FoundersNames"), new string[] { nameof(FoundersNames) }));
        //    }

        //    if (this.FoundersNames?.Length > FoundersNamesMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "FoundersNames", FoundersNamesMaxLength, 1), new string[] { nameof(FoundersNames) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the description.
        ///// </summary>
        //private void ValidateDescription()
        //{
        //    if (string.IsNullOrEmpty(this.Description?.Trim()))
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Description"), new string[] { nameof(Description) }));
        //    }

        //    if (this.Description?.Length > DescriptionMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Description", DescriptionMaxLength, 1), new string[] { nameof(Description) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the curriculum.
        ///// </summary>
        //private void ValidateCurriculum()
        //{
        //    if (string.IsNullOrEmpty(this.Curriculum?.Trim()))
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Curriculum"), new string[] { nameof(Curriculum) }));
        //    }

        //    if (this.Curriculum?.Length > CurriculumMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Curriculum", CurriculumMaxLength, 1), new string[] { nameof(Curriculum) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the business definition.
        ///// </summary>
        //private void ValidateBusinessDefinition()
        //{
        //    if (this.BusinessDefinition?.Length > BusinessDefinitionMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessDefinition", BusinessDefinitionMaxLength, 1), new string[] { nameof(BusinessDefinition) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the website.
        ///// </summary>
        //private void ValidateWebsite()
        //{
        //    if (this.Website?.Length > WebsiteMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Website", WebsiteMaxLength, 1), new string[] { nameof(Website) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the business focus.
        ///// </summary>
        //private void ValidateBusinessFocus()
        //{
        //    if (this.BusinessFocus?.Length > BusinessFocusMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessFocus", BusinessFocusMaxLength, 1), new string[] { nameof(BusinessFocus) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the size of the market.
        ///// </summary>
        //private void ValidateMarketSize()
        //{
        //    if (this.MarketSize?.Length > MarketSizeMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "MarketSize", MarketSizeMaxLength, 1), new string[] { nameof(MarketSize) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the business economic model.
        ///// </summary>
        //private void ValidateBusinessEconomicModel()
        //{
        //    if (this.BusinessEconomicModel?.Length > BusinessEconomicModelMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessEconomicModel", BusinessEconomicModelMaxLength, 1), new string[] { nameof(BusinessEconomicModel) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the business operational model.
        ///// </summary>
        //private void ValidateBusinessOperationalModel()
        //{
        //    if (this.BusinessOperationalModel?.Length > BusinessOperationalModelMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessOperationalModel", BusinessOperationalModelMaxLength, 1), new string[] { nameof(BusinessOperationalModel) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the business differentials.
        ///// </summary>
        //private void ValidateBusinessDifferentials()
        //{
        //    if (this.BusinessDifferentials?.Length > BusinessDifferentialsMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessDifferentials", BusinessDifferentialsMaxLength, 1), new string[] { nameof(BusinessDifferentials) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the competing companies.
        ///// </summary>
        //private void ValidateCompetingCompanies()
        //{
        //    if (this.CompetingCompanies?.Length > CompetingCompaniesMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "CompetingCompanies", CompetingCompaniesMaxLength, 1), new string[] { nameof(CompetingCompanies) }));
        //    }
        //}

        ///// <summary>
        ///// Validates the business stage.
        ///// </summary>
        //private void ValidateBusinessStage()
        //{
        //    if (this.BusinessStage?.Length > BusinessStageMaxLength)
        //    {
        //        this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessStage", BusinessStageMaxLength, 1), new string[] { nameof(BusinessStage) }));
        //    }
        //}

        #endregion
    }
}
