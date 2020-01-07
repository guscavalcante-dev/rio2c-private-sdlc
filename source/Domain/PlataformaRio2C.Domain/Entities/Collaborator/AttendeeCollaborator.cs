// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="AttendeeCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeCollaborator</summary>
    public class AttendeeCollaborator : Entity
    {
        public int EditionId { get; private set; }
        public int CollaboratorId { get; private set; }
        public DateTime? WelcomeEmailSendDate { get; private set; }
        public DateTime? OnboardingStartDate { get; private set; }
        public DateTime? OnboardingFinishDate { get; private set; }
        public DateTime? OnboardingUserDate { get; private set; }
        public DateTime? OnboardingCollaboratorDate { get; private set; }
        public DateTime? OnboardingOrganizationDataSkippedDate { get; private set; }
        public DateTime? PlayerTermsAcceptanceDate { get; private set; }
        public DateTime? ProducerTermsAcceptanceDate { get; private set; }
        public DateTime? SpeakerTermsAcceptanceDate { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Collaborator Collaborator { get; private set; }

        public virtual ICollection<AttendeeCollaboratorType> AttendeeCollaboratorTypes { get; private set; }
        public virtual ICollection<AttendeeOrganizationCollaborator> AttendeeOrganizationCollaborators { get; private set; }
        public virtual ICollection<AttendeeCollaboratorTicket> AttendeeCollaboratorTickets { get; private set; }
        public virtual ICollection<ConferenceParticipant> ConferenceParticipants { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaborator"/> class.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaborator(
            Edition edition, 
            CollaboratorType collaboratorType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            List<AttendeeOrganization> attendeeOrganizations,
            Collaborator collaborator, 
            bool shouldDeleteOrganizations,
            int userId)
        {
            this.Edition = edition;
            this.Collaborator = collaborator;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, shouldDeleteOrganizations, userId);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaborator"/> class for ticket.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="newAttendeeOrganizations">The new attendee organizations.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="jobTitle">The job title.</param>
        /// <param name="barcode">The barcode.</param>
        /// <param name="isBarcodePrinted">if set to <c>true</c> [is barcode printed].</param>
        /// <param name="isBarcodeUsed">if set to <c>true</c> [is barcode used].</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            List<AttendeeOrganization> newAttendeeOrganizations,
            Collaborator collaborator, 
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType, 
            string salesPlatformAttendeeId,
            DateTime salesPlatformUpdateDate,
            string firstName, 
            string lastName, 
            string cellPhone, 
            string jobTitle,
            string barcode,
            bool isBarcodePrinted,
            bool isBarcodeUsed,
            DateTime? barcodeUpdateDate,
            int userId)
        {
            this.Edition = edition;
            this.Collaborator = collaborator;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeAttendeeCollaboratorType(collaboratorType, null, null, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(newAttendeeOrganizations, false, userId);
            this.SynchronizeAttendeeCollaboratorTickets(
                attendeeSalesPlatformTicketType, 
                salesPlatformAttendeeId,
                salesPlatformUpdateDate,
                firstName, 
                lastName, 
                cellPhone, 
                jobTitle,
                barcode,
                isBarcodePrinted,
                isBarcodeUsed,
                barcodeUpdateDate,
                userId);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaborator"/> class.</summary>
        protected AttendeeCollaborator()
        {
        }

        /// <summary>Updates the specified edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            Edition edition, 
            CollaboratorType collaboratorType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            List<AttendeeOrganization> attendeeOrganizations, 
            bool shouldDeleteOrganizations , 
            int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, shouldDeleteOrganizations, userId);
        }

        /// <summary>Deletes the specified collaborator type.</summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(CollaboratorType collaboratorType, int userId)
        {
            this.DeleteAttendeeCollaboratorType(collaboratorType, userId);
            this.DeleteConferenceParticipants(userId);

            if (this.FindAllAttendeeCollaboratorTypesNotDeleted()?.Any() == true)
            {
                return;
            }

            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.DeleteAttendeeOrganizationCollaborators(new List<AttendeeOrganization>(), userId);
        }

        /// <summary>Sends the welcome email send date.</summary>
        /// <param name="userId">The user identifier.</param>
        public void SendWelcomeEmailSendDate(int userId)
        {
            this.WelcomeEmailSendDate = this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        #region Onboarding

        /// <summary>Onboards the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Onboard(int userId)
        {
            if (!this.OnboardingStartDate.HasValue)
            {
                this.OnboardingStartDate = DateTime.Now;
                this.UpdateDate = DateTime.Now;
                this.UpdateUserId = userId;
            }

            if (!this.WelcomeEmailSendDate.HasValue)
            {
                this.WelcomeEmailSendDate = this.OnboardingStartDate;
            }
        }

        /// <summary>Called when [access data].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardAccessData(int userId)
        {
            this.OnboardingUserDate = DateTime.Now;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Called when [player terms acceptance].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardPlayerTermsAcceptance(int userId)
        {
            this.PlayerTermsAcceptanceDate = DateTime.Now;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Called when [speaker terms acceptance].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardSpeakerTermsAcceptance(int userId)
        {
            this.SpeakerTermsAcceptanceDate = DateTime.Now;

            if (!this.OnboardingFinishDate.HasValue)
            {
                this.OnboardingFinishDate = DateTime.Now;
            }

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Called when [producer terms acceptance].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardProducerTermsAcceptance(int userId)
        {
            this.ProducerTermsAcceptanceDate = DateTime.Now;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Called when [data].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardData(int userId)
        {
            this.OnboardingCollaboratorDate = DateTime.Now;

            if (!this.HasCollaboratorType(Constants.CollaboratorType.Speaker) && !this.OnboardingFinishDate.HasValue)
            {
                this.OnboardingFinishDate = DateTime.Now;
            }

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Skips the onboard ticket buyer company data.</summary>
        /// <param name="userId">The user identifier.</param>
        public void SkipOnboardTicketBuyerCompanyData(int userId)
        {
            this.OnboardingOrganizationDataSkippedDate = DateTime.Now;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        #endregion

        #region Attendee Collaborators Types

        /// <summary>Updates the API configuration.</summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">if set to <c>true</c> [is API display enabled].</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateApiConfiguration(CollaboratorType collaboratorType, bool isApiDisplayEnabled, int? apiHighlightPosition, int userId)
        {
            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);

            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the API highlight position.</summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteApiHighlightPosition(CollaboratorType collaboratorType, int userId)
        {
            var attendeeCollaboratorType = this.FindAttendeeCollaboratorTypeByUid(collaboratorType?.Uid ?? Guid.Empty);
            attendeeCollaboratorType?.DeleteApiHighlightPosition(userId);
        }

        /// <summary>Synchronizes the type of the attendee collaborator.</summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeCollaboratorType(CollaboratorType collaboratorType, bool? isApiDisplayEnabled, int? apiHighlightPosition, int userId)
        {
            if (this.AttendeeCollaboratorTypes == null)
            {
                this.AttendeeCollaboratorTypes = new List<AttendeeCollaboratorType>();
            }

            var attendeeCollaboratorType = this.FindAttendeeCollaboratorTypeByUid(collaboratorType?.Uid ?? Guid.Empty);
            if (attendeeCollaboratorType == null)
            {
                this.AttendeeCollaboratorTypes.Add(new AttendeeCollaboratorType(this, collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId));
            }
            else
            {
                attendeeCollaboratorType.Update(isApiDisplayEnabled, apiHighlightPosition, userId);
            }
        }

        /// <summary>Deletes the type of the attendee collaborator.</summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeCollaboratorType(CollaboratorType collaboratorType, int userId)
        {
            var attendeeCollaboratorType = this.FindAttendeeCollaboratorTypeByUid(collaboratorType?.Uid ?? Guid.Empty);
            attendeeCollaboratorType?.Delete(userId);
        }

        /// <summary>Finds the attendee collaborator type by uid.</summary>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <returns></returns>
        private AttendeeCollaboratorType FindAttendeeCollaboratorTypeByUid(Guid collaboratorTypeUid)
        {
            return this.AttendeeCollaboratorTypes?.FirstOrDefault(act => act.CollaboratorType.Uid == collaboratorTypeUid);
        }

        /// <summary>Finds all attendee collaborator types not deleted.</summary>
        /// <returns></returns>
        private List<AttendeeCollaboratorType> FindAllAttendeeCollaboratorTypesNotDeleted()
        {
            return this.AttendeeCollaboratorTypes?.Where(act => !act.IsDeleted)?.ToList();
        }

        /// <summary>Determines whether [has collaborator type] [the specified collaborator type name].</summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <returns>
        ///   <c>true</c> if [has collaborator type] [the specified collaborator type name]; otherwise, <c>false</c>.</returns>
        private bool HasCollaboratorType(string collaboratorTypeName)
        {
            return this.AttendeeCollaboratorTypes?.Any(act => !act.IsDeleted
                                                           && !act.CollaboratorType.IsDeleted
                                                           && act.CollaboratorType.Name == collaboratorTypeName) == true;
        }

        #endregion

        #region Attendee Organization Collaborators

        /// <summary>Synchronizes the attendee organization collaborators.</summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeOrganizationCollaborators(List<AttendeeOrganization> attendeeOrganizations, bool shouldDeleteOrganizations, int userId)
        {
            if (this.AttendeeOrganizationCollaborators == null)
            {
                this.AttendeeOrganizationCollaborators = new List<AttendeeOrganizationCollaborator>();
            }

            if (shouldDeleteOrganizations)
            {
                this.DeleteAttendeeOrganizationCollaborators(attendeeOrganizations, userId);
            }

            if (attendeeOrganizations?.Any() != true)
            {
                return;
            }

            // Create or update descriptions
            foreach (var attendeeOrganizaion in attendeeOrganizations)
            {
                var attendeeOrganizationCollaboratorDb = this.AttendeeOrganizationCollaborators.FirstOrDefault(aoc => aoc.AttendeeOrganizationId == attendeeOrganizaion.Id);
                if (attendeeOrganizationCollaboratorDb != null)
                {
                    attendeeOrganizationCollaboratorDb.Update(userId);
                }
                else
                {
                    this.CreateAttendeeOrganizationCollaborator(attendeeOrganizaion, userId);
                }
            }
        }

        /// <summary>Deletes the attendee organization collaborator.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteAttendeeOrganizationCollaborator(Guid organizationUid, int userId)
        {
            var attendeeOrganizationCollaborator = this.FindAttendeeOrganizationCollaboratorByOrganizationUid(organizationUid);
            attendeeOrganizationCollaborator?.Delete(userId);
        }

        /// <summary>Deletes the attendee organization collaborators.</summary>
        /// <param name="newAttendeeOrganizations">The new attendee organizations.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeOrganizationCollaborators(List<AttendeeOrganization> newAttendeeOrganizations, int userId)
        {
            var attendeeOrganizationCollaboratorToDelete = this.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted 
                                                                                                               && newAttendeeOrganizations?.Select(nao => nao.Id)?.Contains(aoc.AttendeeOrganizationId) == false)
                                                                                                 .ToList();
            foreach (var attendeeOrganizationCollaborator in attendeeOrganizationCollaboratorToDelete)
            {
                attendeeOrganizationCollaborator.Delete(userId);
            }
        }

        /// <summary>Creates the attendee organization collaborator.</summary>
        /// <param name="attendeeOrganization">The attendee organization.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateAttendeeOrganizationCollaborator(AttendeeOrganization attendeeOrganization, int userId)
        {
            this.AttendeeOrganizationCollaborators.Add(new AttendeeOrganizationCollaborator(attendeeOrganization, this, userId));
        }

        private AttendeeOrganizationCollaborator FindAttendeeOrganizationCollaboratorByOrganizationUid(Guid organizationUid)
        {
            return this.AttendeeOrganizationCollaborators?.FirstOrDefault(aoc => aoc.AttendeeOrganization.Organization.Uid == organizationUid);
        }

        /// <summary>Gets all attendee organizations.</summary>
        /// <returns></returns>
        public List<AttendeeOrganization> GetAllAttendeeOrganizations()
        {
            return this.AttendeeOrganizationCollaborators?.Select(aoc => aoc.AttendeeOrganization)?.ToList();
        }

        #endregion

        #region Attendee Collaborator Tickets

        /// <summary>Updates the attendee collaborator ticket.</summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="newAttendeeOrganizations">The new attendee organizations.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="jobTitle">The job title.</param>
        /// <param name="barcode">The barcode.</param>
        /// <param name="isBarcodePrinted">if set to <c>true</c> [is barcode printed].</param>
        /// <param name="isBarcodeUsed">if set to <c>true</c> [is barcode used].</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAttendeeCollaboratorTicket(
            CollaboratorType collaboratorType,
            List<AttendeeOrganization> newAttendeeOrganizations,
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            string salesPlatformAttendeeId,
            DateTime salesPlatformUpdateDate,
            string firstName,
            string lastName,
            string cellPhone,
            string jobTitle,
            string barcode,
            bool isBarcodePrinted,
            bool isBarcodeUsed,
            DateTime? barcodeUpdateDate,
            int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.SynchronizeAttendeeCollaboratorType(collaboratorType, null, null, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(newAttendeeOrganizations, false, userId);
            this.SynchronizeAttendeeCollaboratorTickets(
                attendeeSalesPlatformTicketType, 
                salesPlatformAttendeeId,
                salesPlatformUpdateDate,
                firstName, 
                lastName, 
                cellPhone, 
                jobTitle,
                barcode,
                isBarcodePrinted,
                isBarcodeUsed,
                barcodeUpdateDate,
                userId);
        }

        /// <summary>Synchronizes the attendee collaborator tickets.</summary>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="jobTitle">The job title.</param>
        /// <param name="barcode">The barcode.</param>
        /// <param name="isBarcodePrinted">if set to <c>true</c> [is barcode printed].</param>
        /// <param name="isBarcodeUsed">if set to <c>true</c> [is barcode used].</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeCollaboratorTickets(
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType, 
            string salesPlatformAttendeeId,
            DateTime salesPlatformUpdateDate,
            string firstName, 
            string lastName, 
            string cellPhone, 
            string jobTitle,
            string barcode,
            bool isBarcodePrinted,
            bool isBarcodeUsed,
            DateTime? barcodeUpdateDate,
            int userId)
        {
            if (this.AttendeeCollaboratorTickets == null)
            {
                this.AttendeeCollaboratorTickets = new List<AttendeeCollaboratorTicket>();
            }

            var attendeeCollaboratorTicket = this.AttendeeCollaboratorTickets.FirstOrDefault(act => act.SalesPlatformAttendeeId == salesPlatformAttendeeId);
            if (attendeeCollaboratorTicket == null)
            {
                this.AttendeeCollaboratorTickets.Add(new AttendeeCollaboratorTicket(
                    this, 
                    attendeeSalesPlatformTicketType,
                    salesPlatformAttendeeId,
                    salesPlatformUpdateDate,
                    firstName,
                    lastName,
                    cellPhone,
                    jobTitle,
                    barcode,
                    isBarcodePrinted,
                    isBarcodeUsed,
                    barcodeUpdateDate,
                    userId));
            }
            else
            {
                attendeeCollaboratorTicket.Update(
                    attendeeSalesPlatformTicketType,
                    salesPlatformUpdateDate,
                    firstName, 
                    lastName, 
                    cellPhone, 
                    jobTitle,
                    barcode,
                    isBarcodePrinted,
                    isBarcodeUsed,
                    barcodeUpdateDate,
                    userId);
            }
        }

        /// <summary>Deletes the attendee collaborator ticket.</summary>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteAttendeeCollaboratorTicket(
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            CollaboratorType collaboratorType,
            string salesPlatformAttendeeId,
            DateTime salesPlatformUpdateDate,
            DateTime? barcodeUpdateDate,
            int userId)
        {
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;

            if (this.AttendeeCollaboratorTickets == null)
            {
                return;
            }

            var attendeeCollaboratorTicket = this.AttendeeCollaboratorTickets.FirstOrDefault(act => act.SalesPlatformAttendeeId == salesPlatformAttendeeId 
                                                                                                    && !act.IsDeleted);
            attendeeCollaboratorTicket?.Delete(salesPlatformUpdateDate, barcodeUpdateDate, userId);

            var attendeeCollaboratorTickets = this.FindAllAttendeeCollaboratorTicketsNotDeleted();

            // All tickets are deleted
            if (attendeeCollaboratorTickets?.Any() != true)
            {
                this.IsDeleted = true;
                this.DeleteAttendeeCollaboratorType(collaboratorType, userId);
            }
            // All tickets of the same collaborator type are deleted
            else if (attendeeCollaboratorTickets?.Any(act => act.AttendeeSalesPlatformTicketType.CollaboratorTypeId == collaboratorType.Id) != true)
            {
                this.DeleteAttendeeCollaboratorType(collaboratorType, userId);
            }
        }

        /// <summary>Finds all attendee collaborator tickets not deleted.</summary>
        /// <returns></returns>
        private List<AttendeeCollaboratorTicket> FindAllAttendeeCollaboratorTicketsNotDeleted()
        {
            return this.AttendeeCollaboratorTickets?.Where(act => !act.IsDeleted).ToList();
        }

        #endregion

        #region Conference Participants

        /// <summary>Deletes the conference participants.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteConferenceParticipants(int userId)
        {
            if (this.ConferenceParticipants?.Any() != true)
            {
                return;
            }

            foreach (var conferenceParticipant in this.ConferenceParticipants.Where(c => !c.IsDeleted))
            {
                conferenceParticipant.Delete(userId);
            }
        }

        #endregion

        #region Validations

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            this.ValidateAttendeeCollaboratorTickets();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the attendee collaborator tickets.</summary>
        public void ValidateAttendeeCollaboratorTickets()
        {
            if (this.AttendeeCollaboratorTickets?.Any() != true)
            {
                return;
            }

            foreach (var jobTitle in this.AttendeeCollaboratorTickets?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(jobTitle.ValidationResult);
            }
        }

        #endregion
    }
}