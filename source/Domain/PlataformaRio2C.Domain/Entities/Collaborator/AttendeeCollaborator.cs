// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-21-2023
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
        public virtual ICollection<AttendeeCartoonProjectCollaborator> AttendeeCartoonProjectCollaborators { get; private set; }
        public virtual ICollection<AttendeeInnovationOrganizationCollaborator> AttendeeInnovationOrganizationCollaborators { get; private set; }
        public virtual ICollection<Logistic> Logistics { get; private set; }
        public virtual ICollection<AttendeeCollaboratorInnovationOrganizationTrack> AttendeeCollaboratorInnovationOrganizationTracks { get; private set; }
        public virtual ICollection<AttendeeNegotiationCollaborator> AttendeeNegotiationCollaborators { get; private set; }
        public virtual ICollection<AttendeeCollaboratorInterest> AttendeeCollaboratorInterests { get; private set; }
        public virtual ICollection<AttendeeCollaboratorActivity> AttendeeCollaboratorActivities { get; private set; }
        public virtual ICollection<AttendeeCollaboratorTargetAudience> AttendeeCollaboratorTargetAudiences { get; private set; }

        #region Attendee Collaborator

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

            this.SetCreateDate(userId);

            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, shouldDeleteOrganizations, userId);
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

        #endregion

        #region Ticket Attendee Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaborator" /> class for ticket.
        /// </summary>
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
        /// <param name="ticketUrl">The ticket URL.</param>
        /// <param name="isTicketPrinted">if set to <c>true</c> [is ticket printed].</param>
        /// <param name="isTicketUsed">if set to <c>true</c> [is ticket used].</param>
        /// <param name="ticketUpdateDate">The ticket update date.</param>
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
            string ticketUrl,
            bool isTicketPrinted,
            bool isTicketUsed,
            DateTime? ticketUpdateDate,
            int userId)
        {
            this.Edition = edition;
            this.Collaborator = collaborator;

            this.SetCreateDate(userId);

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
                ticketUrl,
                isTicketPrinted,
                isTicketUsed,
                ticketUpdateDate,
                userId);
        }

        /// <summary>
        /// Updates the ticket attendee collaborator.
        /// </summary>
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
        /// <param name="ticketUrl">The ticket URL.</param>
        /// <param name="isTicketPrinted">if set to <c>true</c> [is ticket printed].</param>
        /// <param name="isTicketUsed">if set to <c>true</c> [is ticket used].</param>
        /// <param name="ticketUpdateDate">The ticket update date.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateTicketAttendeeCollaborator(
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
            string ticketUrl,
            bool isTicketPrinted,
            bool isTicketUsed,
            DateTime? ticketUpdateDate,
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
                ticketUrl,
                isTicketPrinted,
                isTicketUsed,
                ticketUpdateDate,
                userId);
        }

        /// <summary>
        /// Synchronizes the attendee collaborator tickets.
        /// </summary>
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
        /// <param name="ticketUrl">The ticket URL.</param>
        /// <param name="isTicketPrinted">if set to <c>true</c> [is ticket printed].</param>
        /// <param name="isTicketUsed">if set to <c>true</c> [is ticket used].</param>
        /// <param name="ticketUpdateDate">The ticket update date.</param>
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
            string ticketUrl,
            bool isTicketPrinted,
            bool isTicketUsed,
            DateTime? ticketUpdateDate,
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
                    ticketUrl,
                    isTicketPrinted,
                    isTicketUsed,
                    ticketUpdateDate,
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
                    ticketUrl,
                    isTicketPrinted,
                    isTicketUsed,
                    ticketUpdateDate,
                    userId);
            }
        }

        /// <summary>
        /// Deletes the attendee collaborator ticket.
        /// </summary>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="ticketUpdateDate">The ticket update date.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteAttendeeCollaboratorTicket(
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            CollaboratorType collaboratorType,
            string salesPlatformAttendeeId,
            DateTime salesPlatformUpdateDate,
            DateTime? barcodeUpdateDate,
            DateTime? ticketUpdateDate,
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
            attendeeCollaboratorTicket?.Delete(salesPlatformUpdateDate, barcodeUpdateDate, ticketUpdateDate, userId);

            var attendeeCollaboratorTickets = this.FindAllAttendeeCollaboratorTicketsNotDeleted();

            // All tickets of the same collaborator type are deleted
            if (attendeeCollaboratorTickets?.Any(act => act.AttendeeSalesPlatformTicketType.CollaboratorTypeId == collaboratorType.Id) != true)
            {
                this.DeleteAttendeeCollaboratorType(collaboratorType, userId);
            }

            if (this.FindAllAttendeeCollaboratorTypesNotDeleted()?.Any() != true)
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

        #region Innovation Commission Attendee Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaborator"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeInnovationOrganizationTracks">The innovation organization track options.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaborator(
            Edition edition,
            List<AttendeeInnovationOrganizationTrack> attendeeInnovationOrganizationTracks,
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

            this.SetCreateDate(userId);

            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, shouldDeleteOrganizations, userId);
            this.SynchronizeAttendeeCollaboratorInnovationOrganizationTracks(attendeeInnovationOrganizationTracks, userId);
        }

        /// <summary>
        /// Innovations the commission attendee collaborator.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationTracks">The attendee innovation organization tracks.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="userId">The user identifier.</param>
        public void InnovationCommissionAttendeeCollaborator(
            List<AttendeeInnovationOrganizationTrack> attendeeInnovationOrganizationTracks,
            CollaboratorType collaboratorType,
            List<AttendeeOrganization> attendeeOrganizations,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            bool shouldDeleteOrganizations,
            int userId)
        {
            this.SetUpdateDate(userId);

            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, shouldDeleteOrganizations, userId);
            this.SynchronizeAttendeeCollaboratorInnovationOrganizationTracks(attendeeInnovationOrganizationTracks, userId);
        }

        #endregion

        #region Audiovisual Commission Attendee Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaborator"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaborator(
            Edition edition,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
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

            this.SetCreateDate(userId);

            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, shouldDeleteOrganizations, userId);
            this.SynchronizeAttendeeCollaboratorInterests(attendeeCollaboratorInterests, userId);
        }

        /// <summary>
        /// Updates the audiovisual commission attendee collaborator.
        /// </summary>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAudiovisualCommissionAttendeeCollaborator(
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            CollaboratorType collaboratorType,
            List<AttendeeOrganization> attendeeOrganizations,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            bool shouldDeleteOrganizations,
            int userId)
        {
            this.SetUpdateDate(userId);

            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, shouldDeleteOrganizations, userId);
            this.SynchronizeAttendeeCollaboratorInterests(attendeeCollaboratorInterests, userId);
        }

        #endregion

        #region Innovation Player Executive Attendee Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaborator"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="attendeeCollaboratorInnovationOrganizationTracks">The attendee collaborator innovation organization tracks.</param>
        /// <param name="userId">The user identifier.</param>
        private AttendeeCollaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            Collaborator collaborator,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            bool shouldDeleteOrganizations,
            List<AttendeeOrganization> attendeeOrganizations,
            List<AttendeeCollaboratorActivity> attendeeCollaboratorActivities,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            List<AttendeeCollaboratorInnovationOrganizationTrack> attendeeCollaboratorInnovationOrganizationTracks,
            int userId)
        {
            this.Edition = edition;
            this.Collaborator = collaborator;

            this.SetCreateDate(userId);

            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, shouldDeleteOrganizations, userId);
            this.SynchronizeAttendeeCollaboratorActivities(attendeeCollaboratorActivities, userId);
            this.SynchronizeAttendeeCollaboratorInterests(attendeeCollaboratorInterests, userId);
            //TODO: Implementar attendeeCollaboratorInnovationOrganizationTracks
        }

        /// <summary>
        /// Creates the innovation player executive attendee collaborator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="attendeeCollaboratorInnovationOrganizationTracks">The attendee collaborator innovation organization tracks.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static AttendeeCollaborator CreateInnovationPlayerExecutiveAttendeeCollaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            Collaborator collaborator,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            bool shouldDeleteOrganizations,
            List<AttendeeOrganization> attendeeOrganizations,
            List<AttendeeCollaboratorActivity> attendeeCollaboratorActivities,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            List<AttendeeCollaboratorInnovationOrganizationTrack> attendeeCollaboratorInnovationOrganizationTracks,
            int userId)
        {
            return new AttendeeCollaborator(
                edition, 
                collaboratorType, 
                collaborator, 
                isApiDisplayEnabled, 
                apiHighlightPosition,
                shouldDeleteOrganizations,
                attendeeOrganizations, 
                attendeeCollaboratorActivities, 
                attendeeCollaboratorInterests, 
                attendeeCollaboratorInnovationOrganizationTracks, 
                userId);
        }

        /// <summary>
        /// Updates the innovation player executive.
        /// </summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="shouldDeleteOrganizations">if set to <c>true</c> [should delete organizations].</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="attendeeCollaboratorInnovationOrganizationTracks">The attendee collaborator innovation organization tracks.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateInnovationPlayerExecutiveAttendeeCollaborator(
            CollaboratorType collaboratorType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            bool shouldDeleteOrganizations,
            List<AttendeeOrganization> attendeeOrganizations,
            List<AttendeeCollaboratorActivity> attendeeCollaboratorActivities,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            List<AttendeeCollaboratorInnovationOrganizationTrack> attendeeCollaboratorInnovationOrganizationTracks,
            int userId)
        {
            this.SetUpdateDate(userId);

            this.SynchronizeAttendeeCollaboratorType(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, shouldDeleteOrganizations, userId);
            this.SynchronizeAttendeeCollaboratorActivities(attendeeCollaboratorActivities, userId);
            this.SynchronizeAttendeeCollaboratorInterests(attendeeCollaboratorInterests, userId);
            //TODO: Implementar attendeeCollaboratorInnovationOrganizationTracks
        }

        #endregion

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaborator"/> class.</summary>
        protected AttendeeCollaborator()
        {
        }

        /// <summary>
        /// Deletes the specified collaborator type.
        /// </summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(CollaboratorType collaboratorType, OrganizationType organizationType, int userId)
        {
            if (collaboratorType.Name == CollaboratorType.PlayerExecutiveAudiovisual.Name)
            {
                this.DeleteAttendeeCollaboratorType(collaboratorType, userId);
                this.DeleteAttendeeOrganizationCollaborators(organizationType, userId);
            }
            else if (collaboratorType.Name == CollaboratorType.ComissionInnovation.Name)
            {
                this.DeleteAttendeeCollaboratorType(collaboratorType, userId);
                this.DeleteAttendeeInnovationOrganizationEvaluations(userId);
            }
            else if (collaboratorType.Name == CollaboratorType.ComissionMusic.Name)
            {
                this.DeleteAttendeeCollaboratorType(collaboratorType, userId);
                this.DeleteAttendeeMusicBandsEvaluations(userId);
            }
            // Cannot delete AttendeeCollaboratorType for ticket buyers!
            else if (!Constants.CollaboratorType.TicketBuyers.Contains(collaboratorType.Name))
            {
                this.DeleteAttendeeCollaboratorType(collaboratorType, userId);
            }

            if (this.FindAllAttendeeCollaboratorTypesNotDeleted()?.Any() == true)
            {
                return;
            }

            this.DeleteConferenceParticipants(userId);
            this.DeleteAttendeeOrganizationCollaborators(new List<AttendeeOrganization>(), userId);

            base.Delete(userId);
        }

        /// <summary>Sends the welcome email send date.</summary>
        /// <param name="userId">The user identifier.</param>
        public void SendWelcomeEmailSendDate(int userId)
        {
            this.WelcomeEmailSendDate = this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Administrators

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaborator"/> class for Administrator (Partial).
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaborator(
            Edition edition,
            List<CollaboratorType> collaboratorTypes,
            Collaborator collaborator,
            int userId)
        {
            this.Edition = edition;
            this.Collaborator = collaborator;
            this.SynchronizeAdministratorAttendeeCollaboratorTypes(collaboratorTypes, userId);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Updates the administrator.
        /// </summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAdministrator(
            List<CollaboratorType> collaboratorTypes,
            int userId)
        {
            this.SynchronizeAdministratorAttendeeCollaboratorTypes(collaboratorTypes, userId);

            // Deletes the attendee collaborator if no type exists
            if (this.FindAllAttendeeCollaboratorTypesNotDeleted()?.Any() == true)
            {
                this.IsDeleted = false;
            }
            else
            {
                this.IsDeleted = true;
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the administrator.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void DeleteAdministrator(int userId)
        {
            this.DeleteAdministratorAttendeeCollaboratorTypes(new List<CollaboratorType>(), userId);

            if (this.FindAllAttendeeCollaboratorTypesNotDeleted()?.Any() == true)
            {
                return;
            }

            this.DeleteConferenceParticipants(userId);
            this.DeleteAttendeeInnovationOrganizationEvaluations(userId);
            this.DeleteAttendeeOrganizationCollaborators(new List<AttendeeOrganization>(), userId);

            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Synchronizes the administrator attendee collaborator types.
        /// </summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAdministratorAttendeeCollaboratorTypes(List<CollaboratorType> collaboratorTypes, int userId)
        {
            if (this.AttendeeCollaboratorTypes == null)
            {
                this.AttendeeCollaboratorTypes = new List<AttendeeCollaboratorType>();
            }

            this.DeleteAdministratorAttendeeCollaboratorTypes(collaboratorTypes, userId);

            if (collaboratorTypes?.Any() != true)
            {
                return;
            }

            foreach (var collaboratorType in collaboratorTypes)
            {
                var attendeeCollaboratorType = this.FindAttendeeCollaboratorTypeByUid(collaboratorType?.Uid ?? Guid.Empty);
                if (attendeeCollaboratorType == null)
                {
                    this.AttendeeCollaboratorTypes.Add(new AttendeeCollaboratorType(this, collaboratorType, false, null, userId));
                }
                else
                {
                    attendeeCollaboratorType.Update(false, null, userId);
                }
            }
        }

        /// <summary>
        /// Deletes the administrator attendee collaborator types.
        /// </summary>
        /// <param name="newCollaboratorTypes">The new collaborator types.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAdministratorAttendeeCollaboratorTypes(List<CollaboratorType> newCollaboratorTypes, int userId)
        {
            var collaboratorTypesToDelete = this.AttendeeCollaboratorTypes.Where(act => !act.IsDeleted
                                                                                                       && act.CollaboratorType.Role.Name == Constants.Role.AdminPartial
                                                                                                       && newCollaboratorTypes?.Select(nct => nct.Id)?.Contains(act.CollaboratorTypeId) == false)
                .ToList();

            foreach (var attendeeCollaboratorType in collaboratorTypesToDelete)
            {
                attendeeCollaboratorType.Delete(userId);
            }
        }

        #endregion

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

        /// <summary>
        /// Synchronizes the type of the attendee collaborator.
        /// </summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeCollaboratorType(CollaboratorType collaboratorType, bool? isApiDisplayEnabled, int? apiHighlightPosition, int userId)
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

        /// <summary>
        /// Deletes the AttendeeCollaboratorType and if has no any AttendeeCollaboratorTypes, deletes the current AttendeeCollaborator.
        /// </summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteAttendeeCollaboratorTypeAndAttendeeCollaborator(CollaboratorType collaboratorType, int userId)
        {
            this.DeleteAttendeeCollaboratorType(collaboratorType, userId);

            if (this.FindAllAttendeeCollaboratorTypesNotDeleted()?.Any() != true)
            {
                this.IsDeleted = true;
            }
        }

        /// <summary>
        /// Synchronizes the non administrator attendee collaborator types.
        /// </summary>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="shouldDeleteCollaboratorTypes">if set to <c>true</c> [should delete collaborator types].</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeNonAdministratorAttendeeCollaboratorTypes(List<CollaboratorType> collaboratorTypes, bool shouldDeleteCollaboratorTypes, bool? isApiDisplayEnabled, int? apiHighlightPosition, int userId)
        {
            if (this.AttendeeCollaboratorTypes == null)
            {
                this.AttendeeCollaboratorTypes = new List<AttendeeCollaboratorType>();
            }

            if (shouldDeleteCollaboratorTypes)
            {
                this.DeleteNonAdministratorAttendeeCollaboratorTypes(collaboratorTypes, userId);
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

        /// <summary>
        /// Deletes the non administrator attendee collaborator types.
        /// </summary>
        /// <param name="newCollaboratorTypes">The new collaborator types.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteNonAdministratorAttendeeCollaboratorTypes(List<CollaboratorType> newCollaboratorTypes, int userId)
        {
            var collaboratorTypesToDelete = this.AttendeeCollaboratorTypes.Where(act => !act.IsDeleted
                                                                                        && act.CollaboratorType.Role.Name != Constants.Role.AdminPartial
                                                                                        && newCollaboratorTypes?.Select(nct => nct.Id)?.Contains(act.CollaboratorTypeId) == false)
                                                                                        .ToList();
            foreach (var attendeeCollaboratorType in collaboratorTypesToDelete)
            {
                attendeeCollaboratorType.Delete(userId);
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

            // Create or update
            foreach (var attendeeOrganization in attendeeOrganizations)
            {
                var attendeeOrganizationCollaboratorDb = this.AttendeeOrganizationCollaborators.FirstOrDefault(aoc => aoc.AttendeeOrganizationId == attendeeOrganization.Id);
                if (attendeeOrganizationCollaboratorDb != null)
                {
                    attendeeOrganizationCollaboratorDb.Update(userId);
                }
                else
                {
                    this.CreateAttendeeOrganizationCollaborator(attendeeOrganization, userId);
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

        /// <summary>
        /// Deletes the attendee organization collaborators.
        /// </summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeOrganizationCollaborators(OrganizationType organizationType, int userId)
        {
            var attendeeOrganizationCollaboratorToDelete = this.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted
                                                                                                                && aoc.AttendeeOrganization.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                                                                                    && aot.OrganizationType.Uid == organizationType?.Uid)).ToList();

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

        /// <summary>
        /// Finds the attendee organization collaborator by organization uid.
        /// </summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
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
        /// <param name="shouldDeleteAttendeeMusicBandCollaborators">if set to <c>true</c> [should delete music bands].</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeMusicBandCollaborators(List<AttendeeMusicBand> attendeeMusicBands, bool shouldDeleteAttendeeMusicBandCollaborators, int userId)
        {
            if (this.AttendeeMusicBandCollaborators == null)
            {
                this.AttendeeMusicBandCollaborators = new List<AttendeeMusicBandCollaborator>();
            }

            if (shouldDeleteAttendeeMusicBandCollaborators)
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

        #region Attendee Cartoon Projects Collaborators

        /// <summary>Synchronizes the attendee cartoon projects collaborators.</summary>
        /// <param name="attendeeCartoonProjects">The attendee cartoon projects.</param>
        /// <param name="shouldDeleteAttendeeCartoonsProject">if set to <c>true</c> [should delete attende cartoon projects].</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeCartoonProjectsCollaborators(List<AttendeeCartoonProject> attendeeCartoonProjects, bool shouldDeleteAttendeeCartoonsProject, int userId)
        {
            if (this.AttendeeCartoonProjectCollaborators == null)
            {
                this.AttendeeCartoonProjectCollaborators = new List<AttendeeCartoonProjectCollaborator>();
            }

            if (shouldDeleteAttendeeCartoonsProject)
            {
                this.DeleteAttendeeCartoonProjectCollaborators(attendeeCartoonProjects, userId);
            }

            if (attendeeCartoonProjects?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var attendee in attendeeCartoonProjects)
            {
                var attendeeCollaboratorDb = this.AttendeeCartoonProjectCollaborators.FirstOrDefault(ambc => ambc.AttendeeCartoonProjectId == attendee.Id);
                if (attendeeCollaboratorDb != null)
                {
                    attendeeCollaboratorDb.Update(userId);
                }
                else
                {
                    this.CreateAttendeeCartoonProjectCollaborator(attendee, userId);
                }
            }
        }

        /// <summary>Deletes the attendee music band collaborator.</summary>
        /// <param name="musicBandUid">The music band uid.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteAttendeeCartoonProjectCollaborator(Guid musicBandUid, int userId)
        {
            var attendeeMusicBandCollaborator = this.FindAttendeeMusicBandCollaboratorByMusicBandUid(musicBandUid);
            attendeeMusicBandCollaborator?.Delete(userId);
        }

        /// <summary>Deletes the attendee cartoon projects collaborators.</summary>
        /// <param name="newAttendeeCartoonProjects">The new attendee cartoon projects.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeCartoonProjectCollaborators(List<AttendeeCartoonProject> newAttendeeCartoonProjects, int userId)
        {
            var attendeCollaboratorToDelete = this.AttendeeCartoonProjectCollaborators.Where(ambc => !ambc.IsDeleted
                                                                                                             && newAttendeeCartoonProjects?.Select(namb => namb.Id)?.Contains(ambc.AttendeeCartoonProjectId) == false)
                                                                                                 .ToList();
            foreach (var attendee in attendeCollaboratorToDelete)
            {
                attendee.Delete(userId);
            }
        }

        /// <summary>Creates the attendee cartoon project collaborator.</summary>
        /// <param name="attendeeCartoonProject">The attendee cartoon project.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateAttendeeCartoonProjectCollaborator(AttendeeCartoonProject attendeeCartoonProject, int userId)
        {
            this.AttendeeCartoonProjectCollaborators.Add(new AttendeeCartoonProjectCollaborator(attendeeCartoonProject, this, userId));
        }

        /// <summary>Finds the attendee cartoon project collaborator by cartoon project uid.</summary>
        /// <param name="uid">The music band uid.</param>
        /// <returns></returns>
        private AttendeeCartoonProjectCollaborator FindAttendeeCartoonProjectCollaboratorByCartoonProjectUid(Guid uid)
        {
            return this.AttendeeCartoonProjectCollaborators?.FirstOrDefault(ambc => ambc.AttendeeCartoonProject.CartoonProject.Uid == uid);
        }

        /// <summary>Gets all attende cartoon project.</summary>
        /// <returns></returns>
        public List<AttendeeCartoonProject> GetAllAttendeCartoonProjects()
        {
            return this.AttendeeCartoonProjectCollaborators?.Select(ambc => ambc.AttendeeCartoonProject)?.ToList();
        }

        #endregion

        #region Attendee Innovation Organization Collaborators

        /// <summary>Synchronizes the attendee innovation organization collaborators.</summary>
        /// <param name="attendeeInnovationOrganizations">The attendee innovation organizations.</param>
        /// <param name="shouldDeleteAttendeeInnovationOrganizationCollaborators">if set to <c>true</c> [should delete music bands].</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeInnovationOrganizationCollaborators(
            List<AttendeeInnovationOrganization> attendeeInnovationOrganizations,
            bool shouldDeleteAttendeeInnovationOrganizationCollaborators,
            int userId)
        {
            if (this.AttendeeInnovationOrganizationCollaborators == null)
            {
                this.AttendeeInnovationOrganizationCollaborators = new List<AttendeeInnovationOrganizationCollaborator>();
            }

            if (shouldDeleteAttendeeInnovationOrganizationCollaborators)
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

        #region Attendee Collaborator Innovation Organization Tracks

        /// <summary>
        /// Synchronizes the attendee collaborator innovation organization tracks.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationTracks">The attendee innovation organization tracks.</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeCollaboratorInnovationOrganizationTracks(
           List<AttendeeInnovationOrganizationTrack> attendeeInnovationOrganizationTracks,
           int userId)
        {
            if (this.AttendeeCollaboratorInnovationOrganizationTracks == null)
            {
                this.AttendeeCollaboratorInnovationOrganizationTracks = new List<AttendeeCollaboratorInnovationOrganizationTrack>();
            }

            this.DeleteAttendeeCollaboratorInnovationOrganizationTracks(attendeeInnovationOrganizationTracks, userId);

            if (attendeeInnovationOrganizationTracks?.Any() != true)
            {
                return;
            }

            // Create or update
            foreach (var attendeeInnovationOrganizationTrack in attendeeInnovationOrganizationTracks)
            {
                var attendeeCollaboratorInnovationOrganizationTrackDb = this.AttendeeCollaboratorInnovationOrganizationTracks.FirstOrDefault(aciot => aciot.InnovationOrganizationTrackOption.Uid == attendeeInnovationOrganizationTrack.InnovationOrganizationTrackOption?.Uid);
                if (attendeeCollaboratorInnovationOrganizationTrackDb != null)
                {
                    attendeeCollaboratorInnovationOrganizationTrackDb.Update(userId);
                }
                else
                {
                    this.AttendeeCollaboratorInnovationOrganizationTracks.Add(new AttendeeCollaboratorInnovationOrganizationTrack(this, attendeeInnovationOrganizationTrack.InnovationOrganizationTrackOption, userId));
                }
            }
        }

        /// <summary>
        /// Deletes the attendee collaborator innovation organization tracks.
        /// </summary>
        /// <param name="newAttendeeInnovationOrganizationTracks">The new attendee innovation organization tracks.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeCollaboratorInnovationOrganizationTracks(
            List<AttendeeInnovationOrganizationTrack> newAttendeeInnovationOrganizationTracks,
            int userId)
        {
            var attendeeCollaboratorInnovationOrganizationTracksToDelete = this.AttendeeCollaboratorInnovationOrganizationTracks.Where(db => newAttendeeInnovationOrganizationTracks?.Select(ioto => ioto.InnovationOrganizationTrackOption.Uid)?.Contains(db.InnovationOrganizationTrackOption.Uid) == false && !db.IsDeleted).ToList();
            foreach (var attendeeCollaboratorInnovationOrganizationTrack in attendeeCollaboratorInnovationOrganizationTracksToDelete)
            {
                attendeeCollaboratorInnovationOrganizationTrack.Delete(userId);
            }
        }

        #endregion

        #region Attendee Collaborator Activities

        /// <summary>Updates the organization activities.</summary>
        /// <param name="attendeeCollaboratorActivities">The organization activities.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAttendeeCollaboratorActivities(List<AttendeeCollaboratorActivity> attendeeCollaboratorActivities, int userId)
        {
            this.SetUpdateDate(userId);

            this.SynchronizeAttendeeCollaboratorActivities(attendeeCollaboratorActivities, userId);
        }

        /// <summary>Synchronizes the organization activities.</summary>
        /// <param name="attendeeCollaboratorActivities">The organization activities.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeCollaboratorActivities(List<AttendeeCollaboratorActivity> attendeeCollaboratorActivities, int userId)
        {
            if (this.AttendeeCollaboratorActivities == null)
            {
                this.AttendeeCollaboratorActivities = new List<AttendeeCollaboratorActivity>();
            }

            this.DeleteAttendeeCollaboratorActivities(attendeeCollaboratorActivities, userId);

            if (attendeeCollaboratorActivities?.Any() != true)
            {
                return;
            }

            // Create or update activities
            foreach (var attendeeCollaboratorActivity in attendeeCollaboratorActivities)
            {
                var attendeeCollaboratorActivityDb = this.AttendeeCollaboratorActivities.FirstOrDefault(a => a.Activity.Uid == attendeeCollaboratorActivity.Activity.Uid);
                if (attendeeCollaboratorActivityDb != null)
                {
                    attendeeCollaboratorActivityDb.Update(attendeeCollaboratorActivity.AdditionalInfo, userId);
                }
                else
                {
                    this.CreateAttendeeCollaboratorActivity(attendeeCollaboratorActivity.Activity, attendeeCollaboratorActivity.AdditionalInfo, userId);
                }
            }
        }

        /// <summary>
        /// Deletes the attendee collaborator activities.
        /// </summary>
        /// <param name="newAttendeeCollaboratorActivities">The new attendee collaborator activities.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeCollaboratorActivities(List<AttendeeCollaboratorActivity> newAttendeeCollaboratorActivities, int userId)
        {
            var attendeeCollaboratorActivitiesToDelete = this.AttendeeCollaboratorActivities.Where(db => newAttendeeCollaboratorActivities?.Select(oa => oa.Activity.Uid)?.Contains(db.Activity.Uid) == false && !db.IsDeleted).ToList();
            foreach (var attendeeCollaboratorActivityToDelete in attendeeCollaboratorActivitiesToDelete)
            {
                attendeeCollaboratorActivityToDelete.Delete(userId);
            }
        }

        /// <summary>
        /// Creates the attendee collaborator activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        private void CreateAttendeeCollaboratorActivity(Activity activity, string additionalInfo, int userId)
        {
            this.AttendeeCollaboratorActivities.Add(new AttendeeCollaboratorActivity(this, activity, additionalInfo, userId));
        }

        #endregion

        #region Attendee Collaborator Interests

        /// <summary>
        /// Synchronizes the attendeeCollaborator interests.
        /// </summary>
        /// <param name="attendeeCollaboratorInterests">The attendeeCollaborator interests.</param>
        /// <param name="userId">The user identifier.</param>
        public void SynchronizeAttendeeCollaboratorInterests(List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests, int userId)
        {
            if (this.AttendeeCollaboratorInterests == null)
            {
                this.AttendeeCollaboratorInterests = new List<AttendeeCollaboratorInterest>();
            }

            this.DeleteAttendeeCollaboratorInterests(attendeeCollaboratorInterests, userId);

            if (attendeeCollaboratorInterests?.Any() != true)
            {
                return;
            }

            // Create or update interests
            foreach (var attendeeCollaboratorInterest in attendeeCollaboratorInterests)
            {
                var attendeeCollaboratorInterestDb = this.AttendeeCollaboratorInterests.FirstOrDefault(a => a.Interest.Uid == attendeeCollaboratorInterest.Interest.Uid);
                if (attendeeCollaboratorInterestDb != null)
                {
                    attendeeCollaboratorInterestDb.Update(attendeeCollaboratorInterest.AdditionalInfo, userId);
                }
                else
                {
                    this.AttendeeCollaboratorInterests.Add(attendeeCollaboratorInterest);
                }
            }
        }

        /// <summary>
        /// Deletes the attendee collaborator interests.
        /// </summary>
        /// <param name="newAttendeeCollaboratorInterests">The new attendee collaborator interests.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeCollaboratorInterests(List<AttendeeCollaboratorInterest> newAttendeeCollaboratorInterests, int userId)
        {
            var attendeeCollaboratorInterestsToDelete = this.AttendeeCollaboratorInterests.Where(db => newAttendeeCollaboratorInterests?.Select(a => a.Interest.Uid)?.Contains(db.Interest.Uid) == false && !db.IsDeleted).ToList();
            foreach (var attendeeCollaboratorInterestToDelete in attendeeCollaboratorInterestsToDelete)
            {
                attendeeCollaboratorInterestToDelete.Delete(userId);
            }
        }

        #endregion

        #region Attendee Innovation Organization Evaluations

        /// <summary>Deletes the conference participants.</summary>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeInnovationOrganizationEvaluations(int userId)
        {
            if (this.Collaborator?.User?.AttendeeInnovationOrganizationEvaluations?.Any() == true
                && this.Edition.IsInnovationProjectEvaluationOpen())
            {
                foreach (var attendeeInnovationOrganizationEvaluation in this.Collaborator.User.AttendeeInnovationOrganizationEvaluations.Where(c => !c.IsDeleted))
                {
                    attendeeInnovationOrganizationEvaluation.Delete(userId);
                }
            }
        }

        #endregion

        #region Attendee Music Bands Evaluations

        private void DeleteAttendeeMusicBandsEvaluations(int userId)
        {
            if (this.Collaborator?.User?.AttendeeMusicBandEvaluations?.Any() == true
                && this.Edition.IsMusicProjectEvaluationOpen())
            {
                foreach (var attendeeMusicBandEvaluation in this.Collaborator.User.AttendeeMusicBandEvaluations.Where(c => !c.IsDeleted))
                {
                    attendeeMusicBandEvaluation.Delete(userId);
                }
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