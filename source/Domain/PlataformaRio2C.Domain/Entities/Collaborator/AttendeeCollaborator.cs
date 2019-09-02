// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
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
        public DateTime? OnboardingFinishDate { get; private set; }

        public virtual Edition Edition { get; private set; }
        public virtual Collaborator Collaborator { get; private set; }

        public virtual ICollection<AttendeeOrganizationCollaborator> AttendeeOrganizationCollaborators { get; private set; }
        public virtual ICollection<AttendeeCollaboratorTicket> AttendeeCollaboratorTickets { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaborator"/> class for admin.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaborator(Edition edition, List<AttendeeOrganization> attendeeOrganizations, Collaborator collaborator, int userId)
        {
            this.Edition = edition;
            this.Collaborator = collaborator;
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, userId);
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaborator"/> class for ticket.</summary>
        /// <param name="edition">The edition.</param>
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

        /// <summary>Updates the specified edition for admin.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(Edition edition, List<AttendeeOrganization> attendeeOrganizations, int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.SynchronizeAttendeeOrganizationCollaborators(attendeeOrganizations, userId);
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.DeleteAttendeeOrganizationCollaborators(new List<AttendeeOrganization>(), userId);
        }

        #region Attendee Organization Collaborators

        /// <summary>Synchronizes the attendee organization collaborators.</summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeOrganizationCollaborators(List<AttendeeOrganization> attendeeOrganizations, int userId)
        {
            if (this.AttendeeOrganizationCollaborators == null)
            {
                this.AttendeeOrganizationCollaborators = new List<AttendeeOrganizationCollaborator>();
            }

            this.DeleteAttendeeOrganizationCollaborators(attendeeOrganizations, userId);

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

        #endregion

        #region Attendee Collaborator Tickets

        /// <summary>Updates the attendee collaborator ticket.</summary>
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
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteAttendeeCollaboratorTicket(
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
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

            if (this.FindAllAttendeeCollaboratorTicketsNotDeleted()?.Any() != true)
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