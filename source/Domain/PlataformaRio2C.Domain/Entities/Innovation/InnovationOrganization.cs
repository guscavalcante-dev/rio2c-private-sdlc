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
    public class InnovationOrganization : AggregateRoot
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
        private void SynchronizeAttendeeInnovationOrganizationsCollaborators(Edition edition, AttendeeCollaborator attendeeCollaborator, int userId)
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
            return true;
        }

        #endregion
    }
}
