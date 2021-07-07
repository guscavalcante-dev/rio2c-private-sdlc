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

        public virtual WorkDedication WorkDedication { get; private set; }

        public virtual ICollection<InnovationOrganizationOption> InnovationOrganizationOptions { get; private set; }
        public virtual ICollection<AttendeeInnovationOrganization> AttendeeInnovationOrganizations { get; private set; }

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
            Edition edition,
            AttendeeCollaborator attendeeCollaborator,
            WorkDedication workDedication,
            List<InnovationOption> innovationOptions,
            string name,
            string document,
            string serviceName,
            string foundersNames,
            DateTime foundationDate,
            decimal accumulatedRevenue,
            string description,
            string curriculum,
            string businessDefinition,
            string website,
            string businessFocus,
            string marketSize,
            string businessEconomicModel,
            string businessOperationalModel,
            string businessDifferentials,
            string competingCompanies,
            string businessStage,
            bool isPresentationUploaded,
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
            this.BusinessDefinition = businessDefinition;
            this.Website = website;
            this.BusinessFocus = businessFocus;
            this.MarketSize = marketSize;
            this.BusinessEconomicModel = businessEconomicModel;
            this.BusinessOperationalModel = businessOperationalModel;
            this.BusinessDifferentials = businessDifferentials;
            this.CompetingCompanies = competingCompanies;
            this.BusinessStage = businessStage;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
            
            this.UpdatePresentationUploadDate(isPresentationUploaded, false);
            this.SetWorkDedication(workDedication);
            this.AddInnovationOrganizationOptions(innovationOptions, userId);
            this.SynchronizeAttendeeInnovationOrganizationsCollaborators(edition, attendeeCollaborator, userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganization"/> class.
        /// </summary>
        public InnovationOrganization()
        {

        }

        #region Attendee Innovation Organization

        /// <summary>
        /// Synchronizes the attendee innovation organizations collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="musicProjectApiDto">The music project API dto.</param>
        private void SynchronizeAttendeeInnovationOrganizationsCollaborators(
            Edition edition, 
            AttendeeCollaborator attendeeCollaborator, 
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

            var AttendeeInnovationOrganization = this.AttendeeInnovationOrganizations.FirstOrDefault(ao => ao.EditionId == edition.Id);
            if (AttendeeInnovationOrganization != null)
            {
                AttendeeInnovationOrganization.Restore(userId);
                attendeeCollaborator?.SynchronizeAttendeeInnovationOrganizationCollaborators(new List<AttendeeInnovationOrganization> { AttendeeInnovationOrganization }, false, userId);
            }
            else
            {
                var newAttendeeInnovationOrganization = new AttendeeInnovationOrganization(edition, this, userId);
                this.AttendeeInnovationOrganizations.Add(newAttendeeInnovationOrganization);
                attendeeCollaborator?.SynchronizeAttendeeInnovationOrganizationCollaborators(new List<AttendeeInnovationOrganization> { newAttendeeInnovationOrganization }, false, userId);
            }
        }

        /// <summary>Deletes the attendee innovation organization.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeInnovationOrganization(Edition edition, int userId)
        {
            foreach (var AttendeeInnovationOrganization in this.FindAllAttendeeInnovationOrganizationsNotDeleted(edition))
            {
                AttendeeInnovationOrganization?.Delete(userId);
            }
        }

        /// <summary>Gets the attendee organization by edition identifier.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        private AttendeeInnovationOrganization FindAttendeeInnovationOrganizationByEditionId(int editionId)
        {
            return this.AttendeeInnovationOrganizations?.FirstOrDefault(amb => amb.Edition.Id == editionId);
        }

        /// <summary>Finds all attendee innovation organizations not deleted.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns></returns>
        private List<AttendeeInnovationOrganization> FindAllAttendeeInnovationOrganizationsNotDeleted(Edition edition)
        {
            return this.AttendeeInnovationOrganizations?.Where(amb => (edition == null || amb.EditionId == edition.Id) && !amb.IsDeleted)?.ToList();
        }

        #endregion

        #region Innovation Organization Options

        /// <summary>
        /// Adds the innovation organization options.
        /// </summary>
        /// <param name="innovationOptions">The innovation options.</param>
        /// <param name="userId">The user identifier.</param>
        private void AddInnovationOrganizationOptions(List<InnovationOption> innovationOptions, int userId)
        {
            if (this.InnovationOrganizationOptions == null)
            {
                this.InnovationOrganizationOptions = new List<InnovationOrganizationOption>();
            }

            foreach (var innovationOption in innovationOptions)
            {
                this.InnovationOrganizationOptions.Add(new InnovationOrganizationOption(this, innovationOption, null, userId));
            }
        }

        #endregion

        #region Work Dedication

        /// <summary>
        /// Adds the work dedication.
        /// </summary>
        /// <param name="workDedication">The work dedication.</param>
        private void SetWorkDedication(WorkDedication workDedication)
        {
            this.WorkDedication = workDedication;
            this.WorkDedicationId = workDedication.Id;
        }

        #endregion

        /// <summary>
        /// Updates the presentation upload date.
        /// </summary>
        /// <param name="isPresentationUploaded">if set to <c>true</c> [is presentation uploaded].</param>
        /// <param name="isPresentationDeleted">if set to <c>true</c> [is presentation deleted].</param>
        private void UpdatePresentationUploadDate(bool isPresentationUploaded, bool isPresentationDeleted)
        {
            if (isPresentationUploaded)
            {
                this.PresentationUploadDate = DateTime.UtcNow;
            }
            else if (isPresentationDeleted)
            {
                this.PresentationUploadDate = null;
            }
        }

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

            this.ValidateName();
            this.ValidateDocument();
            this.ValidateServiceName();
            this.ValidateFoundersNames();
            this.ValidateDescription();
            this.ValidateCurriculum();
            this.ValidateBusinessDefinition();
            this.ValidateWebsite();
            this.ValidateBusinessFocus();
            this.ValidateMarketSize();
            this.ValidateBusinessEconomicModel();
            this.ValidateBusinessOperationalModel();
            this.ValidateBusinessDifferentials();
            this.ValidateCompetingCompanies();
            this.ValidateBusinessStage();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the name.
        /// </summary>
        private void ValidateName()
        {
            if (string.IsNullOrEmpty(this.Name?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { nameof(Name) }));
            }

            if (this.Name?.Length > NameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, NameMaxLength, 1), new string[] { nameof(Name) }));
            }
        }

        /// <summary>
        /// Validates the document.
        /// </summary>
        private void ValidateDocument()
        {
            if (this.Document?.Length > DocumentMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, DocumentMaxLength, 1), new string[] { nameof(Document) }));
            }
        }

        /// <summary>
        /// Validates the name of the service.
        /// </summary>
        private void ValidateServiceName()
        {
            if (string.IsNullOrEmpty(this.ServiceName?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "ServiceName"), new string[] { nameof(ServiceName) }));
            }

            if (this.ServiceName?.Length > ServiceNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "ServiceName", ServiceNameMaxLength, 1), new string[] { nameof(ServiceName) }));
            }
        }

        /// <summary>
        /// Validates the names of the founders.
        /// </summary>
        private void ValidateFoundersNames()
        {
            if (string.IsNullOrEmpty(this.FoundersNames?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "FoundersNames"), new string[] { nameof(FoundersNames) }));
            }

            if (this.FoundersNames?.Length > FoundersNamesMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "FoundersNames", FoundersNamesMaxLength, 1), new string[] { nameof(FoundersNames) }));
            }
        }

        /// <summary>
        /// Validates the description.
        /// </summary>
        private void ValidateDescription()
        {
            if (string.IsNullOrEmpty(this.Description?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Description"), new string[] { nameof(Description) }));
            }

            if (this.Description?.Length > DescriptionMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Description", DescriptionMaxLength, 1), new string[] { nameof(Description) }));
            }
        }

        /// <summary>
        /// Validates the curriculum.
        /// </summary>
        private void ValidateCurriculum()
        {
            if (string.IsNullOrEmpty(this.Curriculum?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Curriculum"), new string[] { nameof(Curriculum) }));
            }

            if (this.Curriculum?.Length > CurriculumMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Curriculum", CurriculumMaxLength, 1), new string[] { nameof(Curriculum) }));
            }
        }

        /// <summary>
        /// Validates the business definition.
        /// </summary>
        private void ValidateBusinessDefinition()
        {
            if (this.BusinessDefinition?.Length > BusinessDefinitionMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessDefinition", BusinessDefinitionMaxLength, 1), new string[] { nameof(BusinessDefinition) }));
            }
        }

        /// <summary>
        /// Validates the website.
        /// </summary>
        private void ValidateWebsite()
        {
            if (this.Website?.Length > WebsiteMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Website", WebsiteMaxLength, 1), new string[] { nameof(Website) }));
            }
        }

        /// <summary>
        /// Validates the business focus.
        /// </summary>
        private void ValidateBusinessFocus()
        {
            if (this.BusinessFocus?.Length > BusinessFocusMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessFocus", BusinessFocusMaxLength, 1), new string[] { nameof(BusinessFocus) }));
            }
        }

        /// <summary>
        /// Validates the size of the market.
        /// </summary>
        private void ValidateMarketSize()
        {
            if (this.MarketSize?.Length > MarketSizeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "MarketSize", MarketSizeMaxLength, 1), new string[] { nameof(MarketSize) }));
            }
        }

        /// <summary>
        /// Validates the business economic model.
        /// </summary>
        private void ValidateBusinessEconomicModel()
        {
            if (this.BusinessEconomicModel?.Length > BusinessEconomicModelMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessEconomicModel", BusinessEconomicModelMaxLength, 1), new string[] { nameof(BusinessEconomicModel) }));
            }
        }

        /// <summary>
        /// Validates the business operational model.
        /// </summary>
        private void ValidateBusinessOperationalModel()
        {
            if (this.BusinessOperationalModel?.Length > BusinessOperationalModelMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessOperationalModel", BusinessOperationalModelMaxLength, 1), new string[] { nameof(BusinessOperationalModel) }));
            }
        }

        /// <summary>
        /// Validates the business differentials.
        /// </summary>
        private void ValidateBusinessDifferentials()
        {
            if (this.BusinessDifferentials?.Length > BusinessDifferentialsMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessDifferentials", BusinessDifferentialsMaxLength, 1), new string[] { nameof(BusinessDifferentials) }));
            }
        }

        /// <summary>
        /// Validates the competing companies.
        /// </summary>
        private void ValidateCompetingCompanies()
        {
            if (this.CompetingCompanies?.Length > CompetingCompaniesMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "CompetingCompanies", CompetingCompaniesMaxLength, 1), new string[] { nameof(CompetingCompanies) }));
            }
        }

        /// <summary>
        /// Validates the business stage.
        /// </summary>
        private void ValidateBusinessStage()
        {
            if (this.BusinessStage?.Length > BusinessStageMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "BusinessStage", BusinessStageMaxLength, 1), new string[] { nameof(BusinessStage) }));
            }
        }

        #endregion
    }
}
