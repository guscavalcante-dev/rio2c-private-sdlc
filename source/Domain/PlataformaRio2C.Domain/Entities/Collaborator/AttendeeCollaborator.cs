// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
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
        public DateTimeOffset? WelcomeEmailSendDate { get; private set; }
        public DateTimeOffset? OnboardingStartDate { get; private set; }
        public DateTimeOffset? OnboardingFinishDate { get; private set; }
        public DateTimeOffset? OnboardingUserDate { get; private set; }
        public DateTimeOffset? OnboardingCollaboratorDate { get; private set; }
        public DateTimeOffset? OnboardingOrganizationDataSkippedDate { get; private set; }
        public DateTimeOffset? PlayerTermsAcceptanceDate { get; private set; }
        public DateTimeOffset? ProducerTermsAcceptanceDate { get; private set; }
        public DateTimeOffset? SpeakerTermsAcceptanceDate { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Collaborator Collaborator { get; private set; }

        public virtual ICollection<AttendeeCollaboratorType> AttendeeCollaboratorTypes { get; private set; }
        public virtual ICollection<AttendeeOrganizationCollaborator> AttendeeOrganizationCollaborators { get; private set; }
        public virtual ICollection<AttendeeCollaboratorTicket> AttendeeCollaboratorTickets { get; private set; }
        public virtual ICollection<ConferenceParticipant> ConferenceParticipants { get; private set; }
        public virtual ICollection<AttendeeMusicBandCollaborator> AttendeeMusicBandCollaborators { get; private set; }
        public virtual ICollection<AttendeeInnovationOrganizationCollaborator> AttendeeInnovationOrganizationCollaborators { get; private set; }
        public virtual ICollection<Logistic> Logistics { get; private set; }

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
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, shouldDeleteOrganizations, userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaborator"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaborator(
            Edition edition,
            List<CollaboratorType> collaboratorTypes,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            List<AttendeeOrganization> attendeeOrganizations,
            Collaborator collaborator,
            bool shouldDeleteOrganizations,
            bool shouldDeleteCollaboratorTypes,
            int userId)
        {
            this.Edition = edition;
            this.Collaborator = collaborator;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeAttendeeCollaboratorTypes(collaboratorTypes, shouldDeleteCollaboratorTypes, isApiDisplayEnabled, apiHighlightPosition, userId);
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
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
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

        /// <summary>
        /// Updates the specified collaborator type.
        /// </summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">if set to <c>true</c> [is API display enabled].</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            CollaboratorType collaboratorType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            List<AttendeeOrganization> attendeeOrganizations,
            bool shouldDeleteOrganizations,
            int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, shouldDeleteOrganizations, userId);
        }

        /// <summary>
        /// Updates the specified collaborator types.
        /// </summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            List<CollaboratorType> collaboratorTypes,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            List<AttendeeOrganization> attendeeOrganizations,
            bool shouldDeleteOrganizations,
            bool shouldDeleteCollaboratortypes,
            int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.SynchronizeAttendeeCollaboratorTypes(collaboratorTypes, shouldDeleteCollaboratortypes, isApiDisplayEnabled, apiHighlightPosition, userId);
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
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.DeleteAttendeeOrganizationCollaborators(new List<AttendeeOrganization>(), userId);
        }

        /// <summary>
        /// Deletes the specified collaborator types.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            if (this.AttendeeCollaboratorTypes?.Any() == true)
            {
                foreach (var attendeeCollaboratorType in this.FindAllAttendeeCollaboratorTypesNotDeleted())
                {
                    this.DeleteAttendeeCollaboratorType(attendeeCollaboratorType.CollaboratorType, userId);
                }
            }

            this.DeleteConferenceParticipants(userId);

            if (this.FindAllAttendeeCollaboratorTypesNotDeleted()?.Any() == true)
            {
                return;
            }

            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
            this.DeleteAttendeeOrganizationCollaborators(new List<AttendeeOrganization>(), userId);
        }

        /// <summary>Sends the welcome email send date.</summary>
        /// <param name="userId">The user identifier.</param>
        public void SendWelcomeEmailSendDate(int userId)
        {
            this.WelcomeEmailSendDate = this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Onboarding

        /// <summary>Onboards the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Onboard(int userId)
        {
            if (!this.OnboardingStartDate.HasValue)
            {
                this.OnboardingStartDate = DateTime.UtcNow;
                this.UpdateDate = DateTime.UtcNow;
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
            this.OnboardingUserDate = DateTime.UtcNow;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Called when [player terms acceptance].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardPlayerTermsAcceptance(int userId)
        {
            this.PlayerTermsAcceptanceDate = DateTime.UtcNow;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Called when [speaker terms acceptance].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardSpeakerTermsAcceptance(int userId)
        {
            this.SpeakerTermsAcceptanceDate = DateTime.UtcNow;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Called when [producer terms acceptance].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardProducerTermsAcceptance(int userId)
        {
            this.ProducerTermsAcceptanceDate = DateTime.UtcNow;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Called when [data].</summary>
        /// <param name="userId">The user identifier.</param>
        public void OnboardData(int userId)
        {
            this.OnboardingCollaboratorDate = DateTime.UtcNow;

            if (!this.OnboardingFinishDate.HasValue)
            {
                this.OnboardingFinishDate = DateTime.UtcNow;
            }

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Skips the onboard ticket buyer company data.</summary>
        /// <param name="userId">The user identifier.</param>
        public void SkipOnboardTicketBuyerCompanyData(int userId)
        {
            this.OnboardingOrganizationDataSkippedDate = DateTime.UtcNow;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
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

            this.UpdateDate = DateTime.UtcNow;
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
            if (collaboratorType == null || string.IsNullOrEmpty(collaboratorType.Name))
            {
                return;
            }

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

        /// <summary>Synchronizes the type of the attendee collaborator.</summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeCollaboratorTypes(List<CollaboratorType> collaboratorTypes, bool shouldDeleteCollaboratorTypes, bool? isApiDisplayEnabled, int? apiHighlightPosition, int userId)
        {
            if (this.AttendeeCollaboratorTypes == null)
            {
                this.AttendeeCollaboratorTypes = new List<AttendeeCollaboratorType>();
            }

            if (shouldDeleteCollaboratorTypes)
            {
                this.DeleteAttendeeCollaboratorTypes(collaboratorTypes, userId);
            }

            if (collaboratorTypes?.Any() != true)
            {
                return;
            }

            foreach (var collaboratorType in collaboratorTypes)
            {
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
        }

        /// <summary>Deletes the attendee organization collaborators.</summary>
        /// <param name="newCollaboratorTypes">The new attendee organizations.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeCollaboratorTypes(List<CollaboratorType> newCollaboratorTypes, int userId)
        {
            var collaboratorTypesToDelete = this.AttendeeCollaboratorTypes.Where(act => !act.IsDeleted
                                                                                        && newCollaboratorTypes?.Select(nct => nct.Id)?.Contains(act.CollaboratorTypeId) == false)
                                                                                        .ToList();
            foreach (var attendeeCollaboratorType in collaboratorTypesToDelete)
            {
                attendeeCollaboratorType.Delete(userId);
            }
        }



        /// <summary>Finds the attendee collaborator type by uid.</summary>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <returns></returns>
        private AttendeeCollaboratorType FindAttendeeCollaboratorTypeByUid(Guid collaboratorTypeUid)
        {
            return this.AttendeeCollaboratorTypes?.FirstOrDefault(act => act.CollaboratorType.Uid == collaboratorTypeUid);
        }

        /// <summary>
        /// Finds the attendee collaborator type by uid.
        /// </summary>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        private AttendeeCollaboratorType FindAttendeeCollaboratorTypeByUid(Guid collaboratorTypeUid, int attendeeCollaboratorId)
        {
            return this.AttendeeCollaboratorTypes?.FirstOrDefault(act => act.CollaboratorType.Uid == collaboratorTypeUid && act.AttendeeCollaboratorId == attendeeCollaboratorId);
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
            this.UpdateDate = DateTime.UtcNow;
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
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;

            if (this.AttendeeCollaboratorTickets == null)
            {
                return;
            }

            var attendeeCollaboratorTicket = this.AttendeeCollaboratorTickets.FirstOrDefault(act => act.SalesPlatformAttendeeId == salesPlatformAttendeeId
                                                                                                    && !act.IsDeleted);
            attendeeCollaboratorTicket?.Delete(salesPlatformUpdateDate, barcodeUpdateDate, userId);

            var attendeeCollaboratorTickets = this.FindAllAttendeeCollaboratorTicketsNotDeleted();

            // All tickets of the same collaborator type are deleted
            if (attendeeCollaboratorTickets?.Any(act => act.AttendeeSalesPlatformTicketType.CollaboratorTypeId == collaboratorType.Id) != true)
            {
                this.DeleteAttendeeCollaboratorType(collaboratorType, userId);
            }

            var attendeeCollaboratorTypes = this.FindAllAttendeeCollaboratorTypesNotDeleted();
            if (attendeeCollaboratorTypes?.Any() != true)
            {
                this.IsDeleted = true;
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

        #region Attendee Music Band Collaborators

        /// <summary>Synchronizes the attendee music band collaborators.</summary>
        /// <param name="attendeeMusicBands">The attendee music bands.</param>
        /// <param name="shouldDeleteMusicBands">if set to <c>true</c> [should delete music bands].</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeMusicBandCollaborators(List<AttendeeMusicBand> attendeeMusicBands, bool shouldDeleteMusicBands, int userId)
        {
            if (this.AttendeeMusicBandCollaborators == null)
            {
                this.AttendeeMusicBandCollaborators = new List<AttendeeMusicBandCollaborator>();
            }

            if (shouldDeleteMusicBands)
            {
                this.DeleteAttendeeMusicBandCollaborators(attendeeMusicBands, userId);
            }

            if (attendeeMusicBands?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var attendeeMusicBand in attendeeMusicBands)
            {
                var attendeeMusicBandCollaboratorDb = this.AttendeeMusicBandCollaborators.FirstOrDefault(ambc => ambc.AttendeeMusicBandId == attendeeMusicBand.Id);
                if (attendeeMusicBandCollaboratorDb != null)
                {
                    attendeeMusicBandCollaboratorDb.Update(userId);
                }
                else
                {
                    this.CreateAttendeeMusicBandCollaborator(attendeeMusicBand, userId);
                }
            }
        }

        /// <summary>Deletes the attendee music band collaborator.</summary>
        /// <param name="musicBandUid">The music band uid.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteAttendeeMusicBandCollaborator(Guid musicBandUid, int userId)
        {
            var attendeeMusicBandCollaborator = this.FindAttendeeMusicBandCollaboratorByMusicBandUid(musicBandUid);
            attendeeMusicBandCollaborator?.Delete(userId);
        }

        /// <summary>Deletes the attendee music band collaborators.</summary>
        /// <param name="newAttendeeMusicBands">The new attendee music bands.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeMusicBandCollaborators(List<AttendeeMusicBand> newAttendeeMusicBands, int userId)
        {
            var attendeeMusicBandCollaboratorToDelete = this.AttendeeMusicBandCollaborators.Where(ambc => !ambc.IsDeleted
                                                                                                             && newAttendeeMusicBands?.Select(namb => namb.Id)?.Contains(ambc.AttendeeMusicBandId) == false)
                                                                                                 .ToList();
            foreach (var attendeeMusicBandCollaborator in attendeeMusicBandCollaboratorToDelete)
            {
                attendeeMusicBandCollaborator.Delete(userId);
            }
        }

        /// <summary>Creates the attendee music band collaborator.</summary>
        /// <param name="attendeeMusicBand">The attendee music band.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateAttendeeMusicBandCollaborator(AttendeeMusicBand attendeeMusicBand, int userId)
        {
            this.AttendeeMusicBandCollaborators.Add(new AttendeeMusicBandCollaborator(attendeeMusicBand, this, userId));
        }

        /// <summary>Finds the attendee music band collaborator by music band uid.</summary>
        /// <param name="musicBandUid">The music band uid.</param>
        /// <returns></returns>
        private AttendeeMusicBandCollaborator FindAttendeeMusicBandCollaboratorByMusicBandUid(Guid musicBandUid)
        {
            return this.AttendeeMusicBandCollaborators?.FirstOrDefault(ambc => ambc.AttendeeMusicBand.MusicBand.Uid == musicBandUid);
        }

        /// <summary>Gets all attende music bands.</summary>
        /// <returns></returns>
        public List<AttendeeMusicBand> GetAllAttendeMusicBands()
        {
            return this.AttendeeMusicBandCollaborators?.Select(ambc => ambc.AttendeeMusicBand)?.ToList();
        }

        #endregion

        #region Attendee Innovation Organization Collaborators

        /// <summary>Synchronizes the attendee innovation organization collaborators.</summary>
        /// <param name="attendeeInnovationOrganizations">The attendee innovation organizations.</param>
        /// <param name="shouldDeleteInnovationOrganizations">if set to <c>true</c> [should delete music bands].</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeInnovationOrganizationCollaborators(List<AttendeeInnovationOrganization> attendeeInnovationOrganizations, bool shouldDeleteInnovationOrganizations, int userId)
        {
            if (this.AttendeeInnovationOrganizationCollaborators == null)
            {
                this.AttendeeInnovationOrganizationCollaborators = new List<AttendeeInnovationOrganizationCollaborator>();
            }

            if (shouldDeleteInnovationOrganizations)
            {
                this.DeleteAttendeeInnovationOrganizationCollaborators(attendeeInnovationOrganizations, userId);
            }

            if (attendeeInnovationOrganizations?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var attendeeInnovationOrganization in attendeeInnovationOrganizations)
            {
                var attendeeInnovationOrganizationCollaboratorDb = this.AttendeeInnovationOrganizationCollaborators.FirstOrDefault(aioc => aioc.AttendeeInnovationOrganizationId == attendeeInnovationOrganization.Id);
                if (attendeeInnovationOrganizationCollaboratorDb != null)
                {
                    attendeeInnovationOrganizationCollaboratorDb.Update(userId);
                }
                else
                {
                    this.CreateAttendeeInnovationOrganizationCollaborator(attendeeInnovationOrganization, userId);
                }
            }
        }

        /// <summary>Deletes the attendee innovation organization collaborator.</summary>
        /// <param name="InnovationOrganizationUid">The music band uid.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteAttendeeInnovationOrganizationCollaborator(Guid InnovationOrganizationUid, int userId)
        {
            var attendeeInnovationOrganizationCollaborator = this.FindAttendeeInnovationOrganizationCollaboratorByInnovationOrganizationUid(InnovationOrganizationUid);
            attendeeInnovationOrganizationCollaborator?.Delete(userId);
        }

        /// <summary>Deletes the attendee innovation organization collaborators.</summary>
        /// <param name="newAttendeeInnovationOrganizations">The new attendee innovation organizations.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeInnovationOrganizationCollaborators(List<AttendeeInnovationOrganization> newAttendeeInnovationOrganizations, int userId)
        {
            var attendeeInnovationOrganizationCollaboratorToDelete = this.AttendeeInnovationOrganizationCollaborators
                .Where(aioc => !aioc.IsDeleted && newAttendeeInnovationOrganizations?.Select(namb => namb.Id)?.Contains(aioc.AttendeeInnovationOrganizationId) == false)
                .ToList();

            foreach (var attendeeInnovationOrganizationCollaborator in attendeeInnovationOrganizationCollaboratorToDelete)
            {
                attendeeInnovationOrganizationCollaborator.Delete(userId);
            }
        }

        /// <summary>Creates the attendee innovation organization collaborator.</summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateAttendeeInnovationOrganizationCollaborator(AttendeeInnovationOrganization attendeeInnovationOrganization, int userId)
        {
            this.AttendeeInnovationOrganizationCollaborators.Add(new AttendeeInnovationOrganizationCollaborator(attendeeInnovationOrganization, this, userId));
        }

        /// <summary>Finds the attendee innovation organization collaborator by music band uid.</summary>
        /// <param name="InnovationOrganizationUid">The music band uid.</param>
        /// <returns></returns>
        private AttendeeInnovationOrganizationCollaborator FindAttendeeInnovationOrganizationCollaboratorByInnovationOrganizationUid(Guid InnovationOrganizationUid)
        {
            return this.AttendeeInnovationOrganizationCollaborators?.FirstOrDefault(aioc => aioc.AttendeeInnovationOrganization.InnovationOrganization.Uid == InnovationOrganizationUid);
        }

        /// <summary>Gets all attende music bands.</summary>
        /// <returns></returns>
        public List<AttendeeInnovationOrganization> GetAllAttendeInnovationOrganizations()
        {
            return this.AttendeeInnovationOrganizationCollaborators?.Select(aioc => aioc.AttendeeInnovationOrganization)?.ToList();
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