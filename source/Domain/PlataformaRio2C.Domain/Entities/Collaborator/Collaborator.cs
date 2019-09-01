// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="Collaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Collaborator</summary>
    public class Collaborator : AggregateRoot
    {
        public static readonly int FirstNameMinLength = 2;
        public static readonly int FirstNameMaxLength = 100;
        public static readonly int LastNamesMaxLength = 200;
        public static readonly int BadgeMaxLength = 50;
        public static readonly int PhoneNumberMaxLength = 50;
        public static readonly int CellPhoneMaxLength = 50;

        public string FirstName { get; private set; }
        public string LastNames { get; private set; }
        public string Badge { get; private set; }
        public string PhoneNumber { get; private set; }
        public string CellPhone { get; private set; }
        public int? AddressId { get; private set; }
        public DateTime? ImageUploadDate { get; private set; }

        public virtual User User { get; private set; }
        public virtual Address Address { get; private set; }
        public virtual User Updater { get; private set; }

        public virtual ICollection<CollaboratorJobTitle> JobTitles { get; private set; }
        public virtual ICollection<CollaboratorMiniBio> MiniBios { get; private set; }
        public virtual ICollection<AttendeeCollaborator> AttendeeCollaborators { get; private set; }
        //public virtual ICollection<Player> Players { get; private set; }
        //public virtual ICollection<CollaboratorProducer> ProducersEvents { get; private set; }

        //public int? SpeakerId { get;  set; }
        //public int? MusicalCommissionId { get;  set; }
        //public virtual ICollection<Speaker> Speaker { get;  set; }

        /// <summary>Initializes a new instance of the <see cref="Collaborator"/> class for admin.</summary>
        /// <param name="uid">The uid.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="userId">The user identifier.</param>
        public Collaborator(
            Guid uid,
            List<AttendeeOrganization> attendeeOrganizations,
            Edition edition,
            string firstName,
            string lastNames,
            string badge,
            string email,
            string phoneNumber,
            string cellPhone,
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string address2,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded,
            List<CollaboratorJobTitle> jobTitles,
            List<CollaboratorMiniBio> miniBios,
            int userId)
        {
            //this.Uid = uid;
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.Badge = badge?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.UpdateImageUploadDate(isImageUploaded, false);
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeJobTitles(jobTitles, userId);
            this.SynchronizeMiniBios(miniBios, userId);
            this.SynchronizeAttendeeCollaborators(edition, attendeeOrganizations, true, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, address2, addressZipCode, addressIsManual, userId);
            this.UpdateUser(email, null);
        }

        /// <summary>Initializes a new instance of the <see cref="Collaborator"/> class for tickets.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="ticketType">Type of the ticket.</param>
        /// <param name="role">The role.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastMame">The last mame.</param>
        /// <param name="email">The email.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="jobTitle">The job title.</param>
        /// <param name="barcode">The barcode.</param>
        /// <param name="isBarcodePrinted">if set to <c>true</c> [is barcode printed].</param>
        /// <param name="isBarcodeUsed">if set to <c>true</c> [is barcode used].</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="userId">The user identifier.</param>
        public Collaborator(
            Guid collaboratorUid, 
            Edition edition,
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            TicketType ticketType,
            Role role,
            string salesPlatformAttendeeId,
            DateTime salesPlatformUpdateDate,
            string firstName, 
            string lastMame, 
            string email, 
            string cellPhone, 
            string jobTitle,
            string barcode,
            bool isBarcodePrinted,
            bool isBarcodeUsed,
            DateTime? barcodeUpdateDate,
            int userId)
        {
            //this.Uid = collaboratorUid;
            this.FirstName = firstName?.Trim();
            this.LastNames = lastMame?.Trim();
            this.Badge = firstName?.Trim() + (!string.IsNullOrEmpty(lastMame) ? " " + lastMame?.Trim() : string.Empty);
            this.CellPhone = cellPhone?.Trim();
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.Now;
            this.CreateUserId = this.UpdateUserId = userId;
            this.SynchronizeAttendeeCollaborators(
                edition, 
                attendeeSalesPlatformTicketType, 
                salesPlatformAttendeeId,
                salesPlatformUpdateDate,
                firstName, 
                lastMame, 
                cellPhone, 
                jobTitle,
                barcode,
                isBarcodePrinted,
                isBarcodeUsed,
                barcodeUpdateDate,
                userId);
            this.UpdateUser(email, role);
        }

        /// <summary>Initializes a new instance of the <see cref="Collaborator"/> class.</summary>
        protected Collaborator()
        {
        }

        /// <summary>Updates the collaborator for admin.</summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            List<AttendeeOrganization> attendeeOrganizations,
            Edition edition,
            string firstName,
            string lastNames,
            string badge,
            string email,
            string phoneNumber,
            string cellPhone,
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string address2,
            string addressZipCode,
            bool addressIsManual,
            bool isImageUploaded,
            List<CollaboratorJobTitle> jobTitles,
            List<CollaboratorMiniBio> miniBios,
            bool isAddingToCurrentEdition,
            int userId)
        {
            //this.Uid = uid;
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.Badge = badge?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.UpdateImageUploadDate(isImageUploaded, false);
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.SynchronizeJobTitles(jobTitles, userId);
            this.SynchronizeMiniBios(miniBios, userId);
            this.SynchronizeAttendeeCollaborators(edition, attendeeOrganizations, isAddingToCurrentEdition, userId);
            this.UpdateAddress(country, stateUid, stateName, cityUid, cityName, address1, address2, addressZipCode, addressIsManual, userId);
            this.UpdateUser(email, null);
        }

        /// <summary>Deletes the specified edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(Edition edition, int userId)
        {
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.DeleteAttendeeCollaborators(edition, userId);

            if (this.FindAllAttendeeCollaboratorsNotDeleted(edition)?.Any() == false)
            {
                this.IsDeleted = true;
                this.UpdateImageUploadDate(false, true);
                this.Deleteuser();
            }
        }

        /// <summary>Updates the image upload date.</summary>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        private void UpdateImageUploadDate(bool isImageUploaded, bool isImageDeleted)
        {
            if (isImageUploaded)
            {
                this.ImageUploadDate = DateTime.Now;
            }
            else if (isImageDeleted)
            {
                this.ImageUploadDate = null;
            }
        }

        /// <summary>Gets the full name.</summary>
        /// <returns></returns>
        public string GetFullName()
        {
            return this.FirstName + (!string.IsNullOrEmpty(this.LastNames) ? " " + this.LastNames : String.Empty);
        }

        #region Users

        /// <summary>Updates the user.</summary>
        /// <param name="email">The email.</param>
        /// <param name="role">The role.</param>
        public void UpdateUser(string email, Role role)
        {
            if (this.User != null)
            {
                this.User.Update(this.GetFullName(), email, role);
            }
            else
            {
                this.User = new User(this.GetFullName(), email, role);
            }
        }

        /// <summary>Deleteusers the specified user identifier.</summary>
        private void Deleteuser()
        {
            this.User?.Delete();
        }

        #endregion

        #region Addresses

        /// <summary>Updates the address.</summary>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="addressZipCode">The address zip code.</param>
        /// <param name="addressIsManual">if set to <c>true</c> [address is manual].</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAddress(
            Country country,
            Guid? stateUid,
            string stateName,
            Guid? cityUid,
            string cityName,
            string address1,
            string address2,
            string addressZipCode,
            bool addressIsManual,
            int userId)
        {
            if (country == null)
            {
                this.Address?.Delete(userId);
            }
            else if (this.Address == null)
            {
                this.Address = new Address(
                    country,
                    stateUid,
                    stateName,
                    cityUid,
                    cityName,
                    address1,
                    address2,
                    addressZipCode,
                    addressIsManual,
                    userId);
            }
            else
            {
                this.Address.Update(
                    country,
                    stateUid,
                    stateName,
                    cityUid,
                    cityName,
                    address1,
                    address2,
                    addressZipCode,
                    addressIsManual,
                    userId);
            }
        }

        #endregion

        #region Job Titles

        /// <summary>Synchronizes the job titles.</summary>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeJobTitles(List<CollaboratorJobTitle> jobTitles, int userId)
        {
            if (this.JobTitles == null)
            {
                this.JobTitles = new List<CollaboratorJobTitle>();
            }

            this.DeleteJobTitles(jobTitles, userId);

            if (jobTitles?.Any() != true)
            {
                return;
            }

            // Create or update job titles
            foreach (var jobTitle in jobTitles)
            {
                var jobTitleDb = this.JobTitles.FirstOrDefault(d => d.Language.Code == jobTitle.Language.Code);
                if (jobTitleDb != null)
                {
                    jobTitleDb.Update(jobTitle);
                }
                else
                {
                    this.CreateJobTitle(jobTitle);
                }
            }
        }

        /// <summary>Deletes the job titles.</summary>
        /// <param name="newJobTitles">The new job titles.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteJobTitles(List<CollaboratorJobTitle> newJobTitles, int userId)
        {
            var jobTitlesToDelete = this.JobTitles.Where(db => newJobTitles?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false).ToList();
            foreach (var jobTitleToDelete in jobTitlesToDelete)
            {
                jobTitleToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the job title.</summary>
        /// <param name="jobTitle">The job title.</param>
        private void CreateJobTitle(CollaboratorJobTitle jobTitle)
        {
            this.JobTitles.Add(jobTitle);
        }

        #endregion

        #region Mini Bios

        /// <summary>Synchronizes the mini bios.</summary>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeMiniBios(List<CollaboratorMiniBio> miniBios, int userId)
        {
            if (this.MiniBios == null)
            {
                this.MiniBios = new List<CollaboratorMiniBio>();
            }

            this.DeleteMiniBios(miniBios, userId);

            if (miniBios?.Any() != true)
            {
                return;
            }

            // Create or update mini bios
            foreach (var miniBio in miniBios)
            {
                var descriptionDb = this.MiniBios.FirstOrDefault(d => d.Language.Code == miniBio.Language.Code);
                if (descriptionDb != null)
                {
                    descriptionDb.Update(miniBio);
                }
                else
                {
                    this.CreateMiniBios(miniBio);
                }
            }
        }

        /// <summary>Deletes the mini bios.</summary>
        /// <param name="newMiniBios">The new mini bios.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteMiniBios(List<CollaboratorMiniBio> newMiniBios, int userId)
        {
            var miniBiosToDelete = this.MiniBios.Where(db => newMiniBios?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false).ToList();
            foreach (var miniBioToDelete in miniBiosToDelete)
            {
                miniBioToDelete.Delete(userId);
            }
        }

        /// <summary>Creates the mini bios.</summary>
        /// <param name="miniBio">The mini bio.</param>
        private void CreateMiniBios(CollaboratorMiniBio miniBio)
        {
            this.MiniBios.Add(miniBio);
        }

        #endregion

        #region Attendee Collaborators

        #region Admin

        /// <summary>Synchronizes the attendee collaborators.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAttendeeCollaborators(Edition edition, List<AttendeeOrganization> attendeeOrganizations, bool isAddingToCurrentEdition, int userId)
        {
            // Synchronize only when is adding to current edition
            if (!isAddingToCurrentEdition)
            {
                return;
            }

            if (this.AttendeeCollaborators == null)
            {
                this.AttendeeCollaborators = new List<AttendeeCollaborator>();
            }

            if (edition == null)
            {
                return;
            }

            var attendeeCollaborator = this.AttendeeCollaborators.FirstOrDefault(ao => ao.EditionId == edition.Id);
            if (attendeeCollaborator != null)
            {
                attendeeCollaborator.Update(edition, attendeeOrganizations, userId);
            }
            else
            {
                this.AttendeeCollaborators.Add(new AttendeeCollaborator(edition, attendeeOrganizations, this, userId));
            }
        }

        /// <summary>Deletes the attendee collaborators.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeCollaborators(Edition edition, int userId)
        {
            foreach (var attendeeCollaborator in this.FindAllAttendeeCollaboratorsNotDeleted(edition))
            {
                attendeeCollaborator?.Delete(userId);
            }
        }

        /// <summary>Finds all attendee collaborators not deleted.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns></returns>
        private List<AttendeeCollaborator> FindAllAttendeeCollaboratorsNotDeleted(Edition edition)
        {
            return this.AttendeeCollaborators?.Where(ac => (edition == null || ac.EditionId == edition.Id) && !ac.IsDeleted)?.ToList();
        }

        #endregion

        #region Webhook request

        /// <summary>Synchronizes the attendee collaborators.</summary>
        /// <param name="edition">The edition.</param>
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
        private void SynchronizeAttendeeCollaborators(
            Edition edition,
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
            if (this.AttendeeCollaborators == null)
            {
                this.AttendeeCollaborators = new List<AttendeeCollaborator>();
            }

            if (edition == null)
            {
                return;
            }

            var attendeeCollaborator = this.AttendeeCollaborators.FirstOrDefault(ao => ao.EditionId == edition.Id);
            if (attendeeCollaborator != null)
            {
                attendeeCollaborator.UpdateAttendeeCollaboratorTicket(
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
            else
            {
                this.AttendeeCollaborators.Add(new AttendeeCollaborator(
                    edition, 
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
        }

        /// <summary>Deletes the ticket.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteTicket(
            Edition edition,
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            string salesPlatformAttendeeId,
            DateTime salesPlatformUpdateDate,
            DateTime? barcodeUpdateDate,
            int userId)
        {
            if (this.AttendeeCollaborators == null)
            {
                return;
            }

            if (edition == null)
            {
                return;
            }

            var attendeeCollaborator = this.AttendeeCollaborators.FirstOrDefault(ao => ao.EditionId == edition.Id);
            attendeeCollaborator?.DeleteAttendeeCollaboratorTicket(attendeeSalesPlatformTicketType, salesPlatformAttendeeId, salesPlatformUpdateDate, barcodeUpdateDate, userId);
        }

        #endregion

        #region Tickets

        /// <summary>Updates the ticket.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="ticketType">Type of the ticket.</param>
        /// <param name="role">The role.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastMame">The last mame.</param>
        /// <param name="email">The email.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="jobTitle">The job title.</param>
        /// <param name="barcode">The barcode.</param>
        /// <param name="isBarcodePrinted">if set to <c>true</c> [is barcode printed].</param>
        /// <param name="isBarcodeUsed">if set to <c>true</c> [is barcode used].</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateTicket(
            Edition edition,
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            TicketType ticketType,
            Role role,
            string salesPlatformAttendeeId,
            DateTime salesPlatformUpdateDate, 
            string firstName,
            string lastMame,
            string email,
            string cellPhone,
            string jobTitle,
            string barcode,
            bool isBarcodePrinted,
            bool isBarcodeUsed,
            DateTime? barcodeUpdateDate,
            int userId)
        {
            //this.Uid = collaboratorUid;
            this.FirstName = !string.IsNullOrEmpty(this.FirstName) ? this.FirstName : firstName?.Trim();
            this.LastNames = !string.IsNullOrEmpty(this.LastNames) ? this.LastNames : lastMame?.Trim();
            this.Badge = !string.IsNullOrEmpty(this.Badge) ? this.Badge : (firstName?.Trim() + (!string.IsNullOrEmpty(lastMame) ? " " + lastMame?.Trim() : string.Empty));
            this.CellPhone = !string.IsNullOrEmpty(this.CellPhone) ? this.CellPhone : cellPhone?.Trim();
            this.IsDeleted = false;
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            this.SynchronizeAttendeeCollaborators(
                edition, 
                attendeeSalesPlatformTicketType, 
                salesPlatformAttendeeId,
                salesPlatformUpdateDate,
                firstName, 
                lastMame, 
                cellPhone, 
                jobTitle,
                barcode,
                isBarcodePrinted,
                isBarcodeUsed,
                barcodeUpdateDate,
                userId);
            this.UpdateUser(this.User.Email, role);
        }

        /// <summary>Deletes the ticket.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="ticketType">Type of the ticket.</param>
        /// <param name="role">The role.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteTicket(
            Edition edition,
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            TicketType ticketType,
            Role role,
            string salesPlatformAttendeeId,
            DateTime salesPlatformUpdateDate,
            DateTime? barcodeUpdateDate,
            int userId)
        {
            //this.Uid = collaboratorUid;
            this.DeleteTicket(edition, attendeeSalesPlatformTicketType, salesPlatformAttendeeId, salesPlatformUpdateDate, barcodeUpdateDate, userId);
            this.UpdateDate = DateTime.Now;
            this.UpdateUserId = userId;
            //this.DeleteRole(this.User.Email, role);
        }

        #endregion

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

            this.ValidateFirstName();
            this.ValidateLastNames();
            this.ValidateBadge();
            this.ValidatePhoneNumber();
            this.ValidateCellPhone();
            this.ValidateJobTitles();
            this.ValidateMiniBios();
            this.ValidateAddress();
            this.ValidateUser();
            this.ValidateAttendeeCollaborators();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the first name.</summary>
        public void ValidateFirstName()
        {
            if (string.IsNullOrEmpty(this.FirstName?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "FirstName" }));
            }

            if (this.FirstName?.Trim().Length < FirstNameMinLength || this.FirstName?.Trim().Length > FirstNameMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Name, FirstNameMaxLength, FirstNameMinLength), new string[] { "FirstName" }));
            }
        }

        /// <summary>Validates the last names.</summary>
        public void ValidateLastNames()
        {
            if (!string.IsNullOrEmpty(this.LastNames) && this.LastNames?.Trim().Length > LastNamesMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.LastNames, LastNamesMaxLength, 1), new string[] { "LastNames" }));
            }
        }

        /// <summary>Validates the badge.</summary>
        public void ValidateBadge()
        {
            if (!string.IsNullOrEmpty(this.Badge) && this.Badge?.Trim().Length > BadgeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.BadgeName, BadgeMaxLength, 1), new string[] { "BadgeName" }));
            }
        }

        /// <summary>Validates the phone number.</summary>
        public void ValidatePhoneNumber()
        {
            if (!string.IsNullOrEmpty(this.PhoneNumber) && this.PhoneNumber?.Trim().Length > PhoneNumberMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.PhoneNumber, PhoneNumberMaxLength, 1), new string[] { "PhoneNumber" }));
            }
        }

        /// <summary>Validates the cell phone.</summary>
        public void ValidateCellPhone()
        {
            if (!string.IsNullOrEmpty(this.CellPhone) && this.CellPhone?.Trim().Length > CellPhoneMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.CellPhone, CellPhoneMaxLength, 1), new string[] { "CellPhone" }));
            }
        }

        /// <summary>Validates the job titles.</summary>
        public void ValidateJobTitles()
        {
            if (this.JobTitles?.Any() != true)
            {
                return;
            }

            foreach (var jobTitle in this.JobTitles?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(jobTitle.ValidationResult);
            }
        }

        /// <summary>Validates the mini bios.</summary>
        public void ValidateMiniBios()
        {
            if (this.MiniBios?.Any() != true)
            {
                return;
            }

            foreach (var miniBio in this.MiniBios?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(miniBio.ValidationResult);
            }
        }

        /// <summary>Validates the address.</summary>
        public void ValidateAddress()
        {
            if (this.Address != null && !this.Address.IsValid())
            {
                this.ValidationResult.Add(this.Address.ValidationResult);
            }
        }

        /// <summary>Validates the user.</summary>
        public void ValidateUser()
        {
            if (this.User != null && !this.User.IsDeleted)
            {
                this.ValidationResult.Add(this.User.ValidationResult);
            }
        }

        /// <summary>Validates the attendee collaborators.</summary>
        public void ValidateAttendeeCollaborators()
        {
            if (this.AttendeeCollaborators?.Any() != true)
            {
                return;
            }

            foreach (var attendeeCollaborator in this.AttendeeCollaborators?.Where(d => !d.IsValid())?.ToList())
            {
                this.ValidationResult.Add(attendeeCollaborator.ValidationResult);
            }
        }

        #endregion

        #region Old

        public Collaborator(string name, User user)
        {
            //SetName(name);
            //SetUser(user);
        }

        public Collaborator(string name, Player player, User user)
        {
            //SetName(name);
            //SetPlayer(player);
            //SetUser(user);
        }

        public void SetName(string name)
        {
            //Name = name;
        }

        public void SetBadge(string badge)
        {
            //Badge = badge;
        }

        public void SetPhoneNumber(string value)
        {
            //PhoneNumber = value;
        }

        public void SetCellPhone(string value)
        {
            //CellPhone = value;
        }

        public void SetPlayer(Player player)
        {
            //Player = player;
            //if (player != null)
            //{
            //    PlayerId = player.Id;
            //    PlayerUid = player.Uid;
            //}
        }

        public void SetImage(ImageFile image)
        {
            //ImageId = null;
            //Image = image;
            //if (image != null)
            //{
            //    ImageId = image.Id;
            //}
        }

        public void SetPlayerUid(Guid value)
        {
            //PlayerUid = value;
        }

        public void SetUser(User user)
        {
            //User = user;
            //if (user != null)
            //{
            //    UserId = user.Id;
            //}
        }

        public void SetAddress(Address address)
        {
            //Address = address;
        }

        public void SetJobTitles(IEnumerable<CollaboratorJobTitle> jobTitles)
        {
            //JobTitles = jobTitles.ToList();
        }

        public void SetMiniBios(IEnumerable<CollaboratorMiniBio> miniBios)
        {
            //MiniBios = miniBios.ToList();
        }

        public void SetPlayers(IEnumerable<Player> players)
        {
            //if (Players != null)
            //{
            //    Players.Clear();
            //}

            //if (players != null && players.Any())
            //{
            //    Players = players.ToList();
            //}
            //else
            //{
            //    Players = null;
            //}
        }

        public void AddEventsProducers(CollaboratorProducer collaboratorProducer)
        {
            //if (ProducersEvents == null)
            //{
            //    ProducersEvents = new List<CollaboratorProducer> { collaboratorProducer };
            //}
            //else
            //{
            //    ProducersEvents.Add(collaboratorProducer);
            //}
        }

        public string GetJobTitle()
        {
            //CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            //string titlePt = JobTitles.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
            //string titleEn = JobTitles.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

            //if (currentCulture != null && currentCulture.Name == "pt-BR" && !string.IsNullOrWhiteSpace(titlePt))
            //{
            //    return titlePt;
            //}
            //else if (!string.IsNullOrWhiteSpace(titleEn))
            //{
            //    return titleEn;
            //}
            //else
            //{
            //    return null;
            //}

            return null;
        }

        public string GetMiniBio()
        {
            //CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            //string titlePt = MiniBios.Where(e => e.Language.Code == "PtBr").Select(e => e.Value).FirstOrDefault();
            //string titleEn = MiniBios.Where(e => e.Language.Code == "En").Select(e => e.Value).FirstOrDefault();

            //if (currentCulture != null && currentCulture.Name == "pt-BR" && !string.IsNullOrWhiteSpace(titlePt))
            //{
            //    return titlePt;
            //}
            //else if (!string.IsNullOrWhiteSpace(titleEn))
            //{
            //    return titleEn;
            //}
            //else
            //{
            //    return null;
            //}

            return null;
        }

        public string GetCompanyName()
        {
            //if (Players != null && Players.Any())
            //{
            //    return string.Join(", ", Players.Select(e => e.Name));
            //}

            //if (ProducersEvents != null && ProducersEvents.Any())
            //{
            //    return string.Join(", ", ProducersEvents.Select(e => e.Producer.Name));
            //}

            return null;
        }

        //public override bool IsValid()
        //{
        //    ValidationResult = new ValidationResult();

        //    ValidationResult.Add(new CollaboratorIsConsistent().Valid(this));

        //    if (this.User != null)
        //    {
        //        ValidationResult.Add(new UserIsConsistent().Valid(this.User));
        //    }

        //    if (Image != null)
        //    {
        //        ValidationResult.Add(new ImageIsConsistent().Valid(this.Image));
        //        ValidationResult.Add(new CollaboratorImageIsConsistent().Valid(this));
        //    }

        //    return ValidationResult.IsValid;
        //}

        #endregion
    }
}