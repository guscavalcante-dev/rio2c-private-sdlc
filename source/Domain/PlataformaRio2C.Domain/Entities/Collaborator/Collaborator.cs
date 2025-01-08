// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 01-08-2025
// ***********************************************************************
// <copyright file="Collaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using PlataformaRio2C.Domain.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>Collaborator</summary>
    public class Collaborator : AggregateRoot
    {
        public static readonly int FirstNameMinLength = 1;
        public static readonly int FirstNameMaxLength = 100;
        public static readonly int LastNamesMaxLength = 200;
        public static readonly int DocumentMaxLength = 100;
        public static readonly int BadgeMaxLength = 50;
        public static readonly int PhoneNumberMaxLength = 50;
        public static readonly int CellPhoneMaxLength = 50;
        public static readonly int PublicEmailMaxLength = 50;
        public static readonly int WebsiteMaxLength = 300;
        public static readonly int LinkedinMaxLength = 100;
        public static readonly int TwitterMaxLength = 100;
        public static readonly int InstagramMaxLength = 100;
        public static readonly int YoutubeMaxLength = 300;
        public static readonly int SpecialNeedsDescriptionMaxLength = 300;
        public static readonly int CollaboratorGenderAdditionalInfoMaxLength = 300;
        public static readonly int CollaboratorRoleAdditionalInfoMaxLength = 300;
        public static readonly int CollaboratorIndustryAdditionalInfoMaxLength = 300;

        [ToPascalCase]
        public string FirstName { get; protected set; }

        [ToPascalCase]
        public string LastNames { get; protected set; }

        [ToPascalCase]
        public string Badge { get; protected set; }

        public string Document { get; private set; }
        public string PhoneNumber { get; private set; }
        public string CellPhone { get; private set; }
        public string PublicEmail { get; protected set; }
        public string Website { get; private set; }
        public string Linkedin { get; private set; }
        public string Twitter { get; private set; }
        public string Instagram { get; private set; }
        public string Youtube { get; private set; }
        public int? AddressId { get; private set; }
        public DateTimeOffset? ImageUploadDate { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public int? CollaboratorGenderId { get; private set; }
        public string CollaboratorGenderAdditionalInfo { get; private set; }
        public int? CollaboratorRoleId { get; private set; }
        public string CollaboratorRoleAdditionalInfo { get; private set; }
        public int? CollaboratorIndustryId { get; private set; }
        public string CollaboratorIndustryAdditionalInfo { get; private set; }
        public bool? HasAnySpecialNeeds { get; private set; }
        public string SpecialNeedsDescription { get; private set; }
        public string CompanyName { get; protected set; }

        public virtual User User { get; private set; }
        public virtual Address Address { get; private set; }
        public virtual User Creator { get; private set; }
        public virtual User Updater { get; private set; }
        public virtual CollaboratorGender Gender { get; private set; }
        public virtual CollaboratorRole Role { get; private set; }
        public virtual CollaboratorIndustry Industry { get; private set; }

        public virtual ICollection<CollaboratorJobTitle> JobTitles { get; private set; }
        public virtual ICollection<CollaboratorMiniBio> MiniBios { get; private set; }
        public virtual ICollection<AttendeeCollaborator> AttendeeCollaborators { get; protected set; }
        public virtual ICollection<CollaboratorEditionParticipation> EditionParticipantions { get; private set; }

        public Dictionary<string, object> RequiredFieldsToPublish;
        public bool IsAbleToPublishToApi
        {
            get
            {
                this.FillRequiredFieldsToPublishToApi();
                foreach (var requiredField in this.RequiredFieldsToPublish)
                {
                    dynamic val = requiredField.Value;
                    if (val.IsValid == false)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        #region Ticket Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="Collaborator" /> class.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="newAttendeeOrganizations">The new attendee organizations.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
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
        /// <param name="ticketUrl">The ticket URL.</param>
        /// <param name="isTicketPrinted">if set to <c>true</c> [is ticket printed].</param>
        /// <param name="isTicketUsed">if set to <c>true</c> [is ticket used].</param>
        /// <param name="ticketUpdateDate">The ticket update date.</param>
        /// <param name="userId">The user identifier.</param>
        private Collaborator(
            Guid collaboratorUid,
            Edition edition,
            List<AttendeeOrganization> newAttendeeOrganizations,
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            CollaboratorType collaboratorType,
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
            string ticketUrl,
            bool isTicketPrinted,
            bool isTicketUsed,
            DateTime? ticketUpdateDate,
            int userId)
        {
            //this.Uid = collaboratorUid;
            this.FirstName = firstName?.Trim();
            this.LastNames = lastMame?.Trim();
            this.Badge = firstName?.Trim() + (!string.IsNullOrEmpty(lastMame) ? " " + lastMame?.Trim() : string.Empty);
            this.CellPhone = cellPhone?.Trim();
            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeTicketAttendeeCollaborators(
                edition,
                collaboratorType,
                newAttendeeOrganizations,
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
                ticketUrl,
                isTicketPrinted,
                isTicketUsed,
                ticketUpdateDate,
                userId);
            this.UpdateUser(email);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collaborator" /> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastMame">The last mame.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="imageUploadDate">The imageUploadDate.</param>
        /// <param name="jobTitles">The job title</param>
        /// <param name="miniBios">The mini bios</param>
        /// <param name="editionAttendeeCollaboratorBaseDto">The attendee collaborator</param>
        public Collaborator(
            string firstName,
            string lastMame,
            string badge,
            DateTimeOffset? imageUploadDate,
            IEnumerable<CollaboratorJobTitleBaseDto> jobTitles,
            IEnumerable<CollaboratorMiniBioBaseDto> miniBios,
            AttendeeCollaboratorBaseDto editionAttendeeCollaboratorBaseDto
        )
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastMame?.Trim();
            this.Badge = badge?.Trim();
            this.ImageUploadDate = imageUploadDate;
            this.JobTitles = jobTitles.Select(jt =>
                new CollaboratorJobTitle(
                    jt.Value,
                    new Language(
                        jt.LanguageDto.Name,
                        jt.LanguageDto.Code
                    )
                )
            )
            .ToList();
            this.MiniBios = miniBios.Select(mb =>
                new CollaboratorMiniBio(
                    mb.Value,
                    new Language(
                        mb.LanguageDto.Name,
                        mb.LanguageDto.Code
                    )
                )
            )
            .ToList();
            this.AttendeeCollaborators = new Collection<AttendeeCollaborator>();
            if (editionAttendeeCollaboratorBaseDto?.SpeakerTermsAcceptanceDate != null)
            {
                this.AttendeeCollaborators.Add(new AttendeeCollaborator(editionAttendeeCollaboratorBaseDto.SpeakerTermsAcceptanceDate));
            }
        }

        /// <summary>
        /// Creates the collaborator ticket.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="newAttendeeOrganizations">The new attendee organizations.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
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
        /// <param name="ticketUrl">The ticket URL.</param>
        /// <param name="isTicketPrinted">if set to <c>true</c> [is ticket printed].</param>
        /// <param name="isTicketUsed">if set to <c>true</c> [is ticket used].</param>
        /// <param name="ticketUpdateDate">The ticket update date.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Collaborator CreateCollaboratorTicket(
            Guid collaboratorUid,
            Edition edition,
            List<AttendeeOrganization> newAttendeeOrganizations,
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            CollaboratorType collaboratorType,
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
            string ticketUrl,
            bool isTicketPrinted,
            bool isTicketUsed,
            DateTime? ticketUpdateDate,
            int userId)
        {
            return new Collaborator(collaboratorUid,
                edition,
                newAttendeeOrganizations,
                attendeeSalesPlatformTicketType,
                collaboratorType,
                role,
                salesPlatformAttendeeId,
                salesPlatformUpdateDate,
                firstName,
                lastMame,
                email,
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
        /// Updates the ticket.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="newAttendeeOrganizations">The new attendee organizations.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
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
        /// <param name="ticketUrl">The ticket URL.</param>
        /// <param name="isTicketPrinted">if set to <c>true</c> [is ticket printed].</param>
        /// <param name="isTicketUsed">if set to <c>true</c> [is ticket used].</param>
        /// <param name="ticketUpdateDate">The ticket update date.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateTicket(
            Edition edition,
            List<AttendeeOrganization> newAttendeeOrganizations,
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            CollaboratorType collaboratorType,
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
            string ticketUrl,
            bool isTicketPrinted,
            bool isTicketUsed,
            DateTime? ticketUpdateDate,
            int userId)
        {
            //this.Uid = collaboratorUid;
            this.FirstName = !string.IsNullOrEmpty(this.FirstName) ? this.FirstName : firstName?.Trim();
            this.LastNames = !string.IsNullOrEmpty(this.LastNames) ? this.LastNames : lastMame?.Trim();
            this.Badge = !string.IsNullOrEmpty(this.Badge) ? this.Badge : (firstName?.Trim() + (!string.IsNullOrEmpty(lastMame) ? " " + lastMame?.Trim() : string.Empty));
            this.CellPhone = !string.IsNullOrEmpty(this.CellPhone) ? this.CellPhone : cellPhone?.Trim();
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeTicketAttendeeCollaborators(
                edition,
                collaboratorType,
                newAttendeeOrganizations,
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
                ticketUrl,
                isTicketPrinted,
                isTicketUsed,
                ticketUpdateDate,
                userId);
            this.UpdateUser(this.User.Email);
        }

        /// <summary>
        /// Deletes the ticket.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="role">The role.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="ticketUpdateDate">The ticket update date.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteTicket(
            Edition edition,
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            CollaboratorType collaboratorType,
            Role role,
            string salesPlatformAttendeeId,
            DateTime salesPlatformUpdateDate,
            DateTime? barcodeUpdateDate,
            DateTime? ticketUpdateDate,
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
            attendeeCollaborator?.DeleteAttendeeCollaboratorTicket(
                attendeeSalesPlatformTicketType,
                collaboratorType,
                salesPlatformAttendeeId,
                salesPlatformUpdateDate,
                barcodeUpdateDate,
                ticketUpdateDate,
                userId);

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Synchronizes the ticket attendee collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
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
        private void SynchronizeTicketAttendeeCollaborators(
            Edition edition,
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
                attendeeCollaborator.UpdateTicketAttendeeCollaborator(
                    collaboratorType,
                    newAttendeeOrganizations,
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
            else
            {
                this.AttendeeCollaborators.Add(new AttendeeCollaborator(
                    edition,
                    collaboratorType,
                    newAttendeeOrganizations,
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
        }


        #endregion

        #region Administrator Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="Collaborator"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="role">The role.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="userId">The user identifier.</param>
        private Collaborator(
            Edition edition,
            List<CollaboratorType> collaboratorTypes,
            Role role,
            string firstName,
            string lastNames,
            string email,
            string passwordHash,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.SynchronizeAdministratorAttendeeCollaborators(edition, collaboratorTypes, role, userId);
            this.UpdateAdministratorUser(email, passwordHash, role);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Creates the administrator collaborator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="role">The role.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Collaborator CreateAdministratorCollaborator(
            Edition edition,
            List<CollaboratorType> collaboratorTypes,
            Role role,
            string firstName,
            string lastNames,
            string email,
            string passwordHash,
            int userId)
        {
            return new Collaborator(
                edition,
                collaboratorTypes,
                role,
                firstName,
                lastNames,
                email,
                passwordHash,
                userId);
        }

        /// <summary>
        /// Updates the administrator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="role">The role.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAdministrator(
            Edition edition,
            List<CollaboratorType> collaboratorTypes,
            Role role,
            string firstName,
            string lastNames,
            string email,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.SynchronizeAdministratorAttendeeCollaborators(edition, collaboratorTypes, role, userId);
            this.UpdateAdministratorUser(email, null, role);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the administrator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteAdministrator(Edition edition, int userId)
        {
            this.DeleteAdministratorAttendeeCollaborators(edition, userId);
            this.DeleteAdministratorUser();

            if (this.FindAllAttendeeCollaboratorsNotDeleted()?.Any() == false && this.User.Roles?.Any(r => r.Name == Constants.Role.Admin) == false)
            {
                this.IsDeleted = true;
                this.UpdateImageUploadDate(false, true);
                this.DeleteUser();
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Synchronizes the administrator attendee collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="role">The role.</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAdministratorAttendeeCollaborators(
            Edition edition,
            List<CollaboratorType> collaboratorTypes,
            Role role,
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

            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);

            // Clear collaborator types if is admin full
            collaboratorTypes = role.Name == Constants.Role.Admin ? new List<CollaboratorType>() : collaboratorTypes;

            if (attendeeCollaborator != null)
            {
                attendeeCollaborator.UpdateAdministratorAttendeeCollaborator(collaboratorTypes, userId);
            }
            // Create attendee collaborator only if is admin partial
            else if (role.Name == Constants.Role.AdminPartial)
            {
                this.AttendeeCollaborators.Add(AttendeeCollaborator.CreateAdministratorAttendeeCollaborator(edition, collaboratorTypes, this, userId));
            }
        }

        /// <summary>
        /// Updates the administrator user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="role">The role.</param>
        public void UpdateAdministratorUser(string email, string passwordHash, Role role)
        {
            var roles = this.FindAllRolesByAttendeeCollaboratorTypes();

            if (role?.Name == Constants.Role.Admin)
            {
                roles = roles.Union(new Collection<Role> { role })?.ToList();
            }

            if (this.User != null)
            {
                this.User.Update(this.GetFullName(), email, roles, true);
            }
            else
            {
                this.User = new User(this.GetFullName(), email, roles, true);
            }

            if (!string.IsNullOrEmpty(passwordHash))
            {
                this.OnboardUser(passwordHash);
            }
        }

        /// <summary>
        /// Deletes the administrator attendee collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAdministratorAttendeeCollaborators(Edition edition, int userId)
        {
            foreach (var attendeeCollaborator in this.FindAllAttendeeCollaboratorsNotDeleted(edition))
            {
                attendeeCollaborator?.DeleteAdministratorAttendeeCollaborator(userId);
            }
        }

        /// <summary>
        /// Deletes the administrator user.
        /// </summary>
        private void DeleteAdministratorUser()
        {
            this.User?.Delete(new List<Role>(), true);
        }

        #endregion

        #region Tiny Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="Collaborator"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="document">The document.</param>
        /// <param name="userId">The user identifier.</param>
        private Collaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            string firstName,
            string lastNames,
            string email,
            string phoneNumber,
            string cellPhone,
            string document,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.PublicEmail = email?.Trim();

            this.PhoneNumber = phoneNumber?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.Document = document?.Trim();

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeAttendeeCollaborators(edition, collaboratorType, null, null, null, true, userId);
            this.UpdateUser(email);
            this.SetUpdateDate(userId);
        }

        /// <summary>
        /// Creates the tiny collaborator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="document">The document.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Collaborator CreateTinyCollaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            string firstName,
            string lastNames,
            string email,
            string phoneNumber,
            string cellPhone,
            string document,
            int userId)
        {
            return new Collaborator(
                edition,
                collaboratorType,
                firstName,
                lastNames,
                email,
                phoneNumber,
                cellPhone,
                document,
                userId);
        }

        /// <summary>
        /// Updates the tiny collaborator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateTinyCollaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            string firstName,
            string lastNames,
            string email,
            bool isAddingToCurrentEdition,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeAttendeeCollaborators(edition, collaboratorType, null, null, null, isAddingToCurrentEdition, userId);
            this.UpdateUser(email);

            this.SetUpdateDate(userId);
        }

        #endregion

        #region Audiovisual Player Executive Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="Collaborator"/> class.
        /// </summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        /// <param name="hasAnySpecialNeeds">The has any special needs.</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="userId">The user identifier.</param>
        private Collaborator(
            List<AttendeeOrganization> attendeeOrganizations,
            Edition edition,
            CollaboratorType collaboratorType,
            DateTime? birthDate,
            CollaboratorGender collaboratorGender,
            string collaboratorGenderAdditionalInfo,
            CollaboratorRole collaboratorRole,
            string collaboratorRoleAdditionalInfo,
            CollaboratorIndustry collaboratorIndustry,
            string collaboratorIndustryAdditionalInfo,
            bool? hasAnySpecialNeeds,
            string specialNeedsDescription,
            bool? haveYouBeenToRio2CBefore,
            List<Edition> editionsParticipated,
            string firstName,
            string lastNames,
            string badge,
            string email,
            string phoneNumber,
            string cellPhone,
            bool? sharePublicEmail,
            string publicEmail,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
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
            this.UpdatePublicEmail(sharePublicEmail, publicEmail);
            this.UpdateImageUploadDate(isImageUploaded, false);

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;

            this.UpdateEditions(haveYouBeenToRio2CBefore, editionsParticipated, userId);
            this.BirthDate = birthDate;
            this.Gender = collaboratorGender;
            this.Industry = collaboratorIndustry;
            this.Role = collaboratorRole;
            this.CollaboratorGenderId = collaboratorGender?.Id;
            this.CollaboratorGenderAdditionalInfo = collaboratorGenderAdditionalInfo;
            this.CollaboratorRoleId = collaboratorRole?.Id;
            this.CollaboratorRoleAdditionalInfo = collaboratorRoleAdditionalInfo;
            this.CollaboratorIndustryId = collaboratorIndustry?.Id;
            this.CollaboratorIndustryAdditionalInfo = collaboratorIndustryAdditionalInfo;
            this.HasAnySpecialNeeds = hasAnySpecialNeeds;
            this.SpecialNeedsDescription = specialNeedsDescription;

            this.SynchronizeJobTitles(jobTitles, userId);
            this.SynchronizeMiniBios(miniBios, userId);

            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeAttendeeCollaborators(edition, collaboratorType, false, null, attendeeOrganizations, true, userId);
            this.UpdateUser(email);
        }

        /// <summary>
        /// Creates the audiovisual player executive collaborator.
        /// </summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        /// <param name="hasAnySpecialNeeds">The has any special needs.</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Collaborator CreateAudiovisualPlayerExecutiveCollaborator(
            List<AttendeeOrganization> attendeeOrganizations,
            Edition edition,
            CollaboratorType collaboratorType,
            DateTime? birthDate,
            CollaboratorGender collaboratorGender,
            string collaboratorGenderAdditionalInfo,
            CollaboratorRole collaboratorRole,
            string collaboratorRoleAdditionalInfo,
            CollaboratorIndustry collaboratorIndustry,
            string collaboratorIndustryAdditionalInfo,
            bool? hasAnySpecialNeeds,
            string specialNeedsDescription,
            bool? haveYouBeenToRio2CBefore,
            List<Edition> editionsParticipated,
            string firstName,
            string lastNames,
            string badge,
            string email,
            string phoneNumber,
            string cellPhone,
            bool? sharePublicEmail,
            string publicEmail,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            bool isImageUploaded,
            List<CollaboratorJobTitle> jobTitles,
            List<CollaboratorMiniBio> miniBios,
            int userId)
        {
            return new Collaborator(
                attendeeOrganizations,
                edition,
                collaboratorType,
                birthDate,
                collaboratorGender,
                collaboratorGenderAdditionalInfo,
                collaboratorRole,
                collaboratorRoleAdditionalInfo,
                collaboratorIndustry,
                collaboratorIndustryAdditionalInfo,
                hasAnySpecialNeeds,
                specialNeedsDescription,
                haveYouBeenToRio2CBefore,
                editionsParticipated,
                firstName,
                lastNames,
                badge,
                email,
                phoneNumber,
                cellPhone,
                sharePublicEmail,
                publicEmail,
                website,
                linkedin,
                twitter,
                instagram,
                youtube,
                isImageUploaded,
                jobTitles,
                miniBios,
                userId);
        }

        /// <summary>Updates the specified attendee organizations for admin.</summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        /// <param name="hasAnySpecialNeeds">if set to <c>true</c> [has any special needs].</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAudiovisualPlayerExecutive(
            List<AttendeeOrganization> attendeeOrganizations,
            Edition edition,
            CollaboratorType collaboratorType,
            DateTime? birthDate,
            CollaboratorGender collaboratorGender,
            string collaboratorGenderAdditionalInfo,
            CollaboratorRole collaboratorRole,
            string collaboratorRoleAdditionalInfo,
            CollaboratorIndustry collaboratorIndustry,
            string collaboratorIndustryAdditionalInfo,
            bool hasAnySpecialNeeds,
            string specialNeedsDescription,
            bool? haveYouBeenToRio2CBefore,
            List<Edition> editionsParticipated,
            string firstName,
            string lastNames,
            string badge,
            string email,
            string phoneNumber,
            string cellPhone,
            bool? sharePublicEmail,
            string publicEmail,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            bool isImageUploaded,
            bool isImageDeleted,
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
            this.UpdatePublicEmail(sharePublicEmail, publicEmail);
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);

            this.UpdateEditions(haveYouBeenToRio2CBefore, editionsParticipated, userId);
            this.BirthDate = birthDate;
            UpdateGender(collaboratorGender, collaboratorGenderAdditionalInfo);
            UpdateRole(collaboratorRole, collaboratorRoleAdditionalInfo);
            UpdateIndustry(collaboratorIndustry, collaboratorIndustryAdditionalInfo);
            UpdateSpecialNeeds(hasAnySpecialNeeds, specialNeedsDescription);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;

            this.SynchronizeJobTitles(jobTitles, userId);
            this.SynchronizeMiniBios(miniBios, userId);

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeAttendeeCollaborators(edition, collaboratorType, null, null, attendeeOrganizations, isAddingToCurrentEdition, userId);
            this.UpdateUser(email);
        }

        #endregion

        #region Tiny Collaborator & Audiovisual Player Executive Collaborator - Synchronize Attendee Collaborators [Needs refactor]

        /// <summary>Synchronizes the attendee collaborators.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        [Obsolete("This 'SynchronizeAttendeeCollaborators' is used by two different Collaborator creations, and in our new pattern, each Collaborator creation must have your own 'SynchronizeAttendeeCollaborator'. Refactor this!")]
        private void SynchronizeAttendeeCollaborators(
            Edition edition,
            CollaboratorType collaboratorType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            List<AttendeeOrganization> attendeeOrganizations,
            bool isAddingToCurrentEdition,
            int userId)
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

            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            if (attendeeCollaborator != null)
            {
                attendeeCollaborator.Update(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, attendeeOrganizations, true, userId);
            }
            else
            {
                this.AttendeeCollaborators.Add(new AttendeeCollaborator(edition, collaboratorType, isApiDisplayEnabled, apiHighlightPosition, attendeeOrganizations, this, true, userId));
            }
        }

        #endregion

        #region Innovation Player Executive Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="Collaborator" /> class.
        /// </summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        /// <param name="hasAnySpecialNeeds">The has any special needs.</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="attendeeCollaboratorInnovationOrganizationTracks">The attendee collaborator innovation organization tracks.</param>
        /// <param name="userId">The user identifier.</param>
        private Collaborator(
            List<AttendeeOrganization> attendeeOrganizations,
            Edition edition,
            CollaboratorType collaboratorType,
            ProjectType projectType,
            DateTime? birthDate,
            CollaboratorGender collaboratorGender,
            string collaboratorGenderAdditionalInfo,
            CollaboratorRole collaboratorRole,
            string collaboratorRoleAdditionalInfo,
            CollaboratorIndustry collaboratorIndustry,
            string collaboratorIndustryAdditionalInfo,
            bool? hasAnySpecialNeeds,
            string specialNeedsDescription,
            bool? haveYouBeenToRio2CBefore,
            List<Edition> editionsParticipated,
            string firstName,
            string lastNames,
            string badge,
            string email,
            string phoneNumber,
            string cellPhone,
            bool? sharePublicEmail,
            string publicEmail,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            bool isImageUploaded,
            List<CollaboratorJobTitle> jobTitles,
            List<CollaboratorMiniBio> miniBios,
            List<AttendeeCollaboratorActivity> attendeeCollaboratorActivities,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            List<AttendeeCollaboratorInnovationOrganizationTrack> attendeeCollaboratorInnovationOrganizationTracks,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.Badge = badge?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.UpdatePublicEmail(sharePublicEmail, publicEmail);
            this.UpdateImageUploadDate(isImageUploaded, false);
            this.UpdateEditions(haveYouBeenToRio2CBefore, editionsParticipated, userId);

            this.SetCreateDate(userId);

            this.BirthDate = birthDate;
            this.Gender = collaboratorGender;
            this.Industry = collaboratorIndustry;
            this.Role = collaboratorRole;
            this.CollaboratorGenderId = collaboratorGender?.Id;
            this.CollaboratorGenderAdditionalInfo = collaboratorGenderAdditionalInfo;
            this.CollaboratorRoleId = collaboratorRole?.Id;
            this.CollaboratorRoleAdditionalInfo = collaboratorRoleAdditionalInfo;
            this.CollaboratorIndustryId = collaboratorIndustry?.Id;
            this.CollaboratorIndustryAdditionalInfo = collaboratorIndustryAdditionalInfo;
            this.HasAnySpecialNeeds = hasAnySpecialNeeds;
            this.SpecialNeedsDescription = specialNeedsDescription;

            this.SynchronizeJobTitles(jobTitles, userId);
            this.SynchronizeMiniBios(miniBios, userId);

            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.UpdateInnovationPlayerExecutiveAttendeeCollaborators(
                edition,
                collaboratorType,
                projectType,
                false,
                null,
                attendeeOrganizations,
                attendeeCollaboratorActivities,
                attendeeCollaboratorInterests,
                attendeeCollaboratorInnovationOrganizationTracks,
                true,
                userId);

            this.UpdateUser(email);
        }

        /// <summary>
        /// Creates the innovation player executive collaborator.
        /// </summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        /// <param name="hasAnySpecialNeeds">The has any special needs.</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="attendeeCollaboratorInnovationOrganizationTracks">The attendee collaborator innovation organization tracks.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Collaborator CreateInnovationPlayerExecutiveCollaborator(
            List<AttendeeOrganization> attendeeOrganizations,
            Edition edition,
            CollaboratorType collaboratorType,
            ProjectType projectType,
            DateTime? birthDate,
            CollaboratorGender collaboratorGender,
            string collaboratorGenderAdditionalInfo,
            CollaboratorRole collaboratorRole,
            string collaboratorRoleAdditionalInfo,
            CollaboratorIndustry collaboratorIndustry,
            string collaboratorIndustryAdditionalInfo,
            bool? hasAnySpecialNeeds,
            string specialNeedsDescription,
            bool? haveYouBeenToRio2CBefore,
            List<Edition> editionsParticipated,
            string firstName,
            string lastNames,
            string badge,
            string email,
            string phoneNumber,
            string cellPhone,
            bool? sharePublicEmail,
            string publicEmail,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            bool isImageUploaded,
            List<CollaboratorJobTitle> jobTitles,
            List<CollaboratorMiniBio> miniBios,
            List<AttendeeCollaboratorActivity> attendeeCollaboratorActivities,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            List<AttendeeCollaboratorInnovationOrganizationTrack> attendeeCollaboratorInnovationOrganizationTracks,
            int userId)
        {
            return new Collaborator(
                attendeeOrganizations,
                edition,
                collaboratorType,
                projectType,
                birthDate,
                collaboratorGender,
                collaboratorGenderAdditionalInfo,
                collaboratorRole,
                collaboratorRoleAdditionalInfo,
                collaboratorIndustry,
                collaboratorIndustryAdditionalInfo,
                hasAnySpecialNeeds,
                specialNeedsDescription,
                haveYouBeenToRio2CBefore,
                editionsParticipated,
                firstName,
                lastNames,
                badge,
                email,
                phoneNumber,
                cellPhone,
                sharePublicEmail,
                publicEmail,
                website,
                linkedin,
                twitter,
                instagram,
                youtube,
                isImageUploaded,
                jobTitles,
                miniBios,
                attendeeCollaboratorActivities,
                attendeeCollaboratorInterests,
                attendeeCollaboratorInnovationOrganizationTracks,
                userId);
        }

        /// <summary>
        /// Updates the innovation player executive collaborator.
        /// </summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        /// <param name="hasAnySpecialNeeds">if set to <c>true</c> [has any special needs].</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="attendeeCollaboratorInnovationOrganizationTracks">The attendee collaborator innovation organization tracks.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateInnovationPlayerExecutiveCollaborator(
            List<AttendeeOrganization> attendeeOrganizations,
            Edition edition,
            CollaboratorType collaboratorType,
            ProjectType projectType,
            DateTime? birthDate,
            CollaboratorGender collaboratorGender,
            string collaboratorGenderAdditionalInfo,
            CollaboratorRole collaboratorRole,
            string collaboratorRoleAdditionalInfo,
            CollaboratorIndustry collaboratorIndustry,
            string collaboratorIndustryAdditionalInfo,
            bool hasAnySpecialNeeds,
            string specialNeedsDescription,
            bool? haveYouBeenToRio2CBefore,
            List<Edition> editionsParticipated,
            string firstName,
            string lastNames,
            string badge,
            string email,
            string phoneNumber,
            string cellPhone,
            bool? sharePublicEmail,
            string publicEmail,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            bool isImageUploaded,
            bool isImageDeleted,
            List<CollaboratorJobTitle> jobTitles,
            List<CollaboratorMiniBio> miniBios,
            List<AttendeeCollaboratorActivity> attendeeCollaboratorActivities,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            List<AttendeeCollaboratorInnovationOrganizationTrack> attendeeCollaboratorInnovationOrganizationTracks,
            bool isAddingToCurrentEdition,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.Badge = badge?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.BirthDate = birthDate;

            this.UpdatePublicEmail(sharePublicEmail, publicEmail);
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);
            this.UpdateEditions(haveYouBeenToRio2CBefore, editionsParticipated, userId);
            this.UpdateGender(collaboratorGender, collaboratorGenderAdditionalInfo);
            this.UpdateRole(collaboratorRole, collaboratorRoleAdditionalInfo);
            this.UpdateIndustry(collaboratorIndustry, collaboratorIndustryAdditionalInfo);
            this.UpdateSpecialNeeds(hasAnySpecialNeeds, specialNeedsDescription);

            this.SetUpdateDate(userId);

            this.SynchronizeJobTitles(jobTitles, userId);
            this.SynchronizeMiniBios(miniBios, userId);

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.UpdateInnovationPlayerExecutiveAttendeeCollaborators(
                edition,
                collaboratorType,
                projectType,
                null,
                null,
                attendeeOrganizations,
                attendeeCollaboratorActivities,
                attendeeCollaboratorInterests,
                attendeeCollaboratorInnovationOrganizationTracks,
                isAddingToCurrentEdition,
                userId);

            this.UpdateUser(email);
        }

        /// <summary>
        /// Updates the innovation player executive attendee collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="attendeeCollaboratorInnovationOrganizationTracks">The attendee collaborator innovation organization tracks.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        private void UpdateInnovationPlayerExecutiveAttendeeCollaborators(
            Edition edition,
            CollaboratorType collaboratorType,
            ProjectType projectType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            List<AttendeeOrganization> attendeeOrganizations,
            List<AttendeeCollaboratorActivity> attendeeCollaboratorActivities,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            List<AttendeeCollaboratorInnovationOrganizationTrack> attendeeCollaboratorInnovationOrganizationTracks,
            bool isAddingToCurrentEdition,
            int userId)
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

            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            if (attendeeCollaborator != null)
            {
                attendeeCollaborator.UpdateInnovationPlayerExecutiveAttendeeCollaborator(
                    collaboratorType,
                    projectType,
                    isApiDisplayEnabled,
                    apiHighlightPosition,
                    true,
                    attendeeOrganizations,
                    attendeeCollaboratorActivities,
                    attendeeCollaboratorInterests,
                    attendeeCollaboratorInnovationOrganizationTracks,
                    userId);
            }
            else
            {
                this.AttendeeCollaborators.Add(AttendeeCollaborator.CreateInnovationPlayerExecutiveAttendeeCollaborator(
                    edition,
                    collaboratorType,
                    projectType,
                    this,
                    isApiDisplayEnabled,
                    apiHighlightPosition,
                    true,
                    attendeeOrganizations,
                    attendeeCollaboratorActivities,
                    attendeeCollaboratorInterests,
                    attendeeCollaboratorInnovationOrganizationTracks,
                    userId));
            }
        }

        #endregion

        #region Music Player Executive Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="Collaborator"/> class.
        /// </summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        /// <param name="hasAnySpecialNeeds">The has any special needs.</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="isVirtualMeeting">The is virtual meeting.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="attendeeCollaboratorTargetAudiences">The attendee collaborator target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        private Collaborator(
            List<AttendeeOrganization> attendeeOrganizations,
            Edition edition,
            CollaboratorType collaboratorType,
            ProjectType projectType,
            DateTime? birthDate,
            CollaboratorGender collaboratorGender,
            string collaboratorGenderAdditionalInfo,
            CollaboratorRole collaboratorRole,
            string collaboratorRoleAdditionalInfo,
            CollaboratorIndustry collaboratorIndustry,
            string collaboratorIndustryAdditionalInfo,
            bool? hasAnySpecialNeeds,
            string specialNeedsDescription,
            bool? haveYouBeenToRio2CBefore,
            List<Edition> editionsParticipated,
            string firstName,
            string lastNames,
            string badge,
            string email,
            string phoneNumber,
            string cellPhone,
            bool? sharePublicEmail,
            string publicEmail,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            bool isImageUploaded,
            List<CollaboratorJobTitle> jobTitles,
            List<CollaboratorMiniBio> miniBios,
            bool? isVirtualMeeting,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.Badge = badge?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.UpdatePublicEmail(sharePublicEmail, publicEmail);
            this.UpdateImageUploadDate(isImageUploaded, false);
            this.UpdateEditions(haveYouBeenToRio2CBefore, editionsParticipated, userId);

            this.SetCreateDate(userId);

            this.BirthDate = birthDate;
            this.Gender = collaboratorGender;
            this.Industry = collaboratorIndustry;
            this.Role = collaboratorRole;
            this.CollaboratorGenderId = collaboratorGender?.Id;
            this.CollaboratorGenderAdditionalInfo = collaboratorGenderAdditionalInfo;
            this.CollaboratorRoleId = collaboratorRole?.Id;
            this.CollaboratorRoleAdditionalInfo = collaboratorRoleAdditionalInfo;
            this.CollaboratorIndustryId = collaboratorIndustry?.Id;
            this.CollaboratorIndustryAdditionalInfo = collaboratorIndustryAdditionalInfo;
            this.HasAnySpecialNeeds = hasAnySpecialNeeds;
            this.SpecialNeedsDescription = specialNeedsDescription;

            this.SynchronizeJobTitles(jobTitles, userId);
            this.SynchronizeMiniBios(miniBios, userId);

            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!  
            //todo: passar o isVirtualMeeting aqui
            this.UpdateMusicPlayerExecutiveAttendeeCollaborators(
               edition,
               collaboratorType,
               projectType,
               false,
               null,
               attendeeOrganizations,
               isVirtualMeeting,
               true,
               userId);

            this.UpdateUser(email);
        }

        /// <summary>
        /// Creates the music player executive.
        /// </summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        /// <param name="hasAnySpecialNeeds">The has any special needs.</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="isVirtualMeeting">The is virtual meeting.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="attendeeCollaboratorTargetAudiences">The attendee collaborator target audiences.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Collaborator CreateMusicPlayerExecutive(
            List<AttendeeOrganization> attendeeOrganizations,
            Edition edition,
            CollaboratorType collaboratorType,
            ProjectType projectType,
            DateTime? birthDate,
            CollaboratorGender collaboratorGender,
            string collaboratorGenderAdditionalInfo,
            CollaboratorRole collaboratorRole,
            string collaboratorRoleAdditionalInfo,
            CollaboratorIndustry collaboratorIndustry,
            string collaboratorIndustryAdditionalInfo,
            bool? hasAnySpecialNeeds,
            string specialNeedsDescription,
            bool? haveYouBeenToRio2CBefore,
            List<Edition> editionsParticipated,
            string firstName,
            string lastNames,
            string badge,
            string email,
            string phoneNumber,
            string cellPhone,
            bool? sharePublicEmail,
            string publicEmail,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            bool isImageUploaded,
            List<CollaboratorJobTitle> jobTitles,
            List<CollaboratorMiniBio> miniBios,
            bool? isVirtualMeeting,
            int userId)
        {
            return new Collaborator(
               attendeeOrganizations,
               edition,
               collaboratorType,
               projectType,
               birthDate,
               collaboratorGender,
               collaboratorGenderAdditionalInfo,
               collaboratorRole,
               collaboratorRoleAdditionalInfo,
               collaboratorIndustry,
               collaboratorIndustryAdditionalInfo,
               hasAnySpecialNeeds,
               specialNeedsDescription,
               haveYouBeenToRio2CBefore,
               editionsParticipated,
               firstName,
               lastNames,
               badge,
               email,
               phoneNumber,
               cellPhone,
               sharePublicEmail,
               publicEmail,
               website,
               linkedin,
               twitter,
               instagram,
               youtube,
               isImageUploaded,
               jobTitles,
               miniBios,
               isVirtualMeeting,
               userId);
        }

        /// <summary>Updates the specified attendee organizations for admin.</summary>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        /// <param name="hasAnySpecialNeeds">if set to <c>true</c> [has any special needs].</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMusicPlayerExecutive(
            List<AttendeeOrganization> attendeeOrganizations,
            Edition edition,
            CollaboratorType collaboratorType,
            ProjectType projectType,
            DateTime? birthDate,
            CollaboratorGender collaboratorGender,
            string collaboratorGenderAdditionalInfo,
            CollaboratorRole collaboratorRole,
            string collaboratorRoleAdditionalInfo,
            CollaboratorIndustry collaboratorIndustry,
            string collaboratorIndustryAdditionalInfo,
            bool hasAnySpecialNeeds,
            string specialNeedsDescription,
            bool? haveYouBeenToRio2CBefore,
            List<Edition> editionsParticipated,
            string firstName,
            string lastNames,
            string badge,
            string email,
            string phoneNumber,
            string cellPhone,
            bool? sharePublicEmail,
            string publicEmail,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            bool isImageUploaded,
            bool isImageDeleted,
            List<CollaboratorJobTitle> jobTitles,
            List<CollaboratorMiniBio> miniBios,
            bool isAddingToCurrentEdition,
            bool? isVirtualMeeting,
            int userId)
        {
            //this.Uid = uid;
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.Badge = badge?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.UpdatePublicEmail(sharePublicEmail, publicEmail);
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.UpdateSocialNetworks(website, linkedin, twitter, instagram, youtube, userId);

            this.UpdateEditions(haveYouBeenToRio2CBefore, editionsParticipated, userId);
            this.BirthDate = birthDate;
            UpdateGender(collaboratorGender, collaboratorGenderAdditionalInfo);
            UpdateRole(collaboratorRole, collaboratorRoleAdditionalInfo);
            UpdateIndustry(collaboratorIndustry, collaboratorIndustryAdditionalInfo);
            UpdateSpecialNeeds(hasAnySpecialNeeds, specialNeedsDescription);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;

            this.SynchronizeJobTitles(jobTitles, userId);
            this.SynchronizeMiniBios(miniBios, userId);


            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.UpdateMusicPlayerExecutiveAttendeeCollaborators(
                edition,
                collaboratorType,
                projectType,
                null,
                null,
                attendeeOrganizations,
                isVirtualMeeting,
                isAddingToCurrentEdition,
                userId);
        }

        /// <summary>
        /// Updates the innovation player executive attendee collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="isApiDisplayEnabled">The is API display enabled.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="attendeeOrganizations">The attendee organizations.</param>
        /// <param name="isVirtualMeeting">The is virtual meeting.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="attendeeCollaboratorTargetAudiences">The attendee collaborator music organization tracks.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        private void UpdateMusicPlayerExecutiveAttendeeCollaborators(
            Edition edition,
            CollaboratorType collaboratorType,
            ProjectType projectType,
            bool? isApiDisplayEnabled,
            int? apiHighlightPosition,
            List<AttendeeOrganization> attendeeOrganizations,
            bool? isVirtualMeeting,
            bool isAddingToCurrentEdition,
            int userId)
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

            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            if (attendeeCollaborator != null)
            {
                //todo: passar o isVirtualMeeting aqui
                attendeeCollaborator.UpdateMusicPlayerExecutiveAttendeeCollaborator(
                    collaboratorType,
                    projectType,
                    isApiDisplayEnabled,
                    apiHighlightPosition,
                    true,
                    attendeeOrganizations,
                    userId);
            }
            else
            {
                //todo: passar o isVirtualMeeting aqui
                this.AttendeeCollaborators.Add(AttendeeCollaborator.CreateMusicPlayerExecutiveAttendeeCollaborator(
                    edition,
                    collaboratorType,
                    projectType,
                    this,
                    isApiDisplayEnabled,
                    apiHighlightPosition,
                    true,
                    attendeeOrganizations,
                    userId));
            }
        }

        #endregion

        #region Base Commission Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="Collaborator"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        private Collaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            string firstName,
            string lastNames,
            string email,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.PublicEmail = email?.Trim();

            this.SetCreateDate(userId);

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeBaseCommissionAttendeeCollaborators(edition, collaboratorType, true, userId);
            this.UpdateUser(email);
        }

        /// <summary>
        /// Creates the base commission collaborator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Collaborator CreateBaseCommissionCollaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            string firstName,
            string lastNames,
            string email,
            int userId)
        {
            return new Collaborator(
                edition,
                collaboratorType,
                firstName,
                lastNames,
                email,
                userId);
        }

        /// <summary>
        /// Updates the base commission collaborator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateBaseCommissionCollaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            bool isAddingToCurrentEdition,
            string firstName,
            string lastNames,
            string email,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.PublicEmail = email?.Trim();

            this.SetUpdateDate(userId);

            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeBaseCommissionAttendeeCollaborators(edition, collaboratorType, isAddingToCurrentEdition, userId);
            this.UpdateUser(email);
        }

        /// <summary>
        /// Synchronizes the base commission attendee collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeBaseCommissionAttendeeCollaborators(
            Edition edition,
            CollaboratorType collaboratorType,
            bool isAddingToCurrentEdition,
            int userId)
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

            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            if (attendeeCollaborator != null)
            {
                attendeeCollaborator.UpdateBaseCommissionAttendeeCollaborator(collaboratorType, userId);
            }
            else
            {
                this.AttendeeCollaborators.Add(AttendeeCollaborator.CreateBaseCommissionAttendeeCollaborator(edition, collaboratorType, this, userId));
            }
        }

        #endregion

        #region Innovation Commission Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="Collaborator" /> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="attendeeInnovationOrganizationTracks">The attendee innovation organization tracks.</param>
        /// <param name="userId">The user identifier.</param>
        private Collaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            string firstName,
            string lastNames,
            string email,
            List<AttendeeInnovationOrganizationTrack> attendeeInnovationOrganizationTracks,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.PublicEmail = email?.Trim();

            this.SetCreateDate(userId);

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeInnovationCommissionAttendeeCollaborators(edition, collaboratorType, attendeeInnovationOrganizationTracks, true, userId);
            this.UpdateUser(email);
        }

        /// <summary>
        /// Creates the innovation commission collaborator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="attendeeInnovationOrganizationTracks">The attendee innovation organization tracks.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Collaborator CreateInnovationCommissionCollaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            string firstName,
            string lastNames,
            string email,
            List<AttendeeInnovationOrganizationTrack> attendeeInnovationOrganizationTracks,
            int userId)
        {
            return new Collaborator(
                edition,
                collaboratorType,
                firstName,
                lastNames,
                email,
                attendeeInnovationOrganizationTracks,
                userId);
        }

        /// <summary>
        /// Updates the innovation commission.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="attendeeInnovationOrganizationTracks">The attendee innovation organization tracks.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateInnovationCommissionCollaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            bool isAddingToCurrentEdition,
            string firstName,
            string lastNames,
            string email,
            List<AttendeeInnovationOrganizationTrack> attendeeInnovationOrganizationTracks,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.PublicEmail = email?.Trim();

            this.SetUpdateDate(userId);

            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeInnovationCommissionAttendeeCollaborators(edition, collaboratorType, attendeeInnovationOrganizationTracks, isAddingToCurrentEdition, userId);
            this.UpdateUser(email);
        }

        /// <summary>
        /// Synchronizes the innovation commission attendee collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="attendeeInnovationOrganizationTracks">The attendee innovation organization tracks.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeInnovationCommissionAttendeeCollaborators(
            Edition edition,
            CollaboratorType collaboratorType,
            List<AttendeeInnovationOrganizationTrack> attendeeInnovationOrganizationTracks,
            bool isAddingToCurrentEdition,
            int userId)
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

            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            if (attendeeCollaborator != null)
            {
                attendeeCollaborator.UpdateInnovationCommissionAttendeeCollaborator(attendeeInnovationOrganizationTracks, collaboratorType, userId);
            }
            else
            {
                this.AttendeeCollaborators.Add(AttendeeCollaborator.CreateInnovationCommissionAttendeeCollaborator(edition, attendeeInnovationOrganizationTracks, collaboratorType, this, userId));
            }
        }

        #endregion

        #region Audiovisual Commission Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="Collaborator"/> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="userId">The user identifier.</param>
        private Collaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            ProjectType projectType,
            string firstName,
            string lastNames,
            string email,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            int userId)
        {
            //this.Uid = uid;
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.PublicEmail = email?.Trim();

            this.SetCreateDate(userId);

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeAudiovisualCommissionAttendeeCollaborators(edition, collaboratorType, projectType, attendeeCollaboratorInterests, true, userId);
            this.UpdateUser(email);
        }

        /// <summary>
        /// Creates the audiovisual commission collaborator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Collaborator CreateAudiovisualCommissionCollaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            ProjectType projectType,
            string firstName,
            string lastNames,
            string email,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            int userId)
        {
            return new Collaborator(
                edition,
                collaboratorType,
                projectType,
                firstName,
                lastNames,
                email,
                attendeeCollaboratorInterests,
                userId);
        }

        /// <summary>
        /// Updates the audiovisual commission.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="attendeeCollaboratorInterests">The commission attendee collaborator interests.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAudiovisualCommissionCollaborator(
            Edition edition,
            CollaboratorType collaboratorType,
            bool isAddingToCurrentEdition,
            ProjectType projectType,
            string firstName,
            string lastNames,
            string email,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.PublicEmail = email?.Trim();

            this.SetUpdateDate(userId);

            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeAudiovisualCommissionAttendeeCollaborators(edition, collaboratorType, projectType, attendeeCollaboratorInterests, isAddingToCurrentEdition, userId);
            this.UpdateUser(email);
        }

        /// <summary>
        /// Synchronizes the attendee collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="attendeeCollaboratorInterests">The commission attendee collaborator interests.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeAudiovisualCommissionAttendeeCollaborators(
            Edition edition,
            CollaboratorType collaboratorType,
            ProjectType projectType,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            bool isAddingToCurrentEdition,
            int userId)
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

            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            if (attendeeCollaborator != null)
            {
                attendeeCollaborator.UpdateAudiovisualCommissionAttendeeCollaborator(attendeeCollaboratorInterests, collaboratorType, projectType, userId);
            }
            else
            {
                this.AttendeeCollaborators.Add(AttendeeCollaborator.CreateAudiovisualCommissionAttendeeCollaborator(edition, attendeeCollaboratorInterests, collaboratorType, projectType, this, userId));
            }
        }

        #endregion

        #region Music Commission Collaborator

        /// <summary>
        /// Initializes a new instance of the <see cref="Collaborator" /> class.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorTypes">The collaborator types.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        private Collaborator(
            Edition edition,
            List<CollaboratorType> collaboratorTypes,
            string firstName,
            string lastNames,
            string email,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.PublicEmail = email?.Trim();

            this.SetCreateDate(userId);

            //TODO: Refactor this!
            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeMusicCommissionAttendeeCollaborators(edition, collaboratorTypes, true, userId);
            this.UpdateUser(email);
        }

        /// <summary>
        /// Creates the music commission collaborator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static Collaborator CreateMusicCommissionCollaborator(
            Edition edition,
            List<CollaboratorType> collaboratorTypes,
            string firstName,
            string lastNames,
            string email,
            int userId)
        {
            return new Collaborator(
                edition,
                collaboratorTypes,
                firstName,
                lastNames,
                email,
                userId);
        }

        /// <summary>
        /// Updates the music commission collaborator.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorTypes">Type of the collaborator.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateMusicCommissionCollaborator(
            Edition edition,
            List<CollaboratorType> collaboratorTypes,
            bool isAddingToCurrentEdition,
            string firstName,
            string lastNames,
            string email,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.PublicEmail = email?.Trim();

            this.SetUpdateDate(userId);

            //BE CAREFUL! Always call "SynchronizeAttendeeCollaborators before "UpdateUser", because "UpdateUser" require informations setted in "SynchronizeAttendeeCollaborators"!
            this.SynchronizeMusicCommissionAttendeeCollaborators(edition, collaboratorTypes, isAddingToCurrentEdition, userId);
            this.UpdateUser(email);
        }

        /// <summary>
        /// Synchronizes the innovation commission attendee collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isAddingToCurrentEdition">if set to <c>true</c> [is adding to current edition].</param>
        /// <param name="userId">The user identifier.</param>
        private void SynchronizeMusicCommissionAttendeeCollaborators(
            Edition edition,
            List<CollaboratorType> collaboratorTypes,
            bool isAddingToCurrentEdition,
            int userId)
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

            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            if (attendeeCollaborator != null)
            {
                attendeeCollaborator.UpdateMusicCommissionAttendeeCollaborator(collaboratorTypes, userId);
            }
            else
            {
                this.AttendeeCollaborators.Add(AttendeeCollaborator.CreateMusicCommissionAttendeeCollaborator(edition, collaboratorTypes, this, userId));
            }
        }

        #endregion

        /// <summary>Initializes a new instance of the <see cref="Collaborator"/> class.</summary>
        protected Collaborator()
        {
        }

        #region Users

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="passwordHash">The password hash.</param>
        public void UpdateUser(string email, string passwordHash)
        {
            if (this.User != null)
            {
                this.User.Update(this.GetFullName(), email, this.FindAllRolesByAttendeeCollaboratorTypes(), false);
            }
            else
            {
                this.User = new User(this.GetFullName(), email, this.FindAllRolesByAttendeeCollaboratorTypes(), false);
            }

            this.OnboardUser(passwordHash);
        }

        /// <summary>Updates the user.</summary>
        /// <param name="email">The email.</param>
        public void UpdateUser(string email)
        {
            if (this.User != null)
            {
                this.User.Update(this.GetFullName(), email, this.FindAllRolesByAttendeeCollaboratorTypes(), false);
            }
            else
            {
                this.User = new User(this.GetFullName(), email, this.FindAllRolesByAttendeeCollaboratorTypes(), false);
            }
        }

        #region Privates

        /// <summary>
        /// Deletes the user.
        /// </summary>
        private void DeleteUser()
        {
            this.User?.Delete(this.FindAllRolesByAttendeeCollaboratorTypes(), false);
        }

        /// <summary>Called when [user].</summary>
        /// <param name="passwordHash">The password hash.</param>
        private void OnboardUser(string passwordHash)
        {
            this.User.OnboardAccessData(this.GetFullName(), passwordHash);
        }

        #endregion

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
            var jobTitlesToDelete = this.JobTitles.Where(db => newJobTitles?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
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
            var miniBiosToDelete = this.MiniBios.Where(db => newMiniBios?.Select(d => d.Language.Code)?.Contains(db.Language.Code) == false && !db.IsDeleted).ToList();
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

        #region Deletes

        /// <summary>
        /// Deletes the specified edition.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(Edition edition, CollaboratorType collaboratorType, OrganizationType organizationType, int userId)
        {
            this.DeleteAttendeeCollaborators(edition, collaboratorType, organizationType, userId);
            this.DeleteUser();

            // Delete only if the collaborator has no attendee collaborators in any edition and is not admin
            if (this.FindAllAttendeeCollaboratorsNotDeleted()?.Any() == false && this.User.Roles?.Any(r => r.Name == Constants.Role.Admin) == false)
            {
                this.IsDeleted = true;
                this.UpdateImageUploadDate(false, true);
            }

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the API highlight position.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteApiHighlightPosition(Edition edition, CollaboratorType collaboratorType, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition?.Id ?? 0);
            attendeeCollaborator?.DeleteApiHighlightPosition(collaboratorType, userId);

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Deletes the organization.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteOrganization(Edition edition, Guid organizationUid, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.DeleteAttendeeOrganizationCollaborator(organizationUid, userId);

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Privates

        /// <summary>
        /// Deletes the attendee collaborators.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteAttendeeCollaborators(Edition edition, CollaboratorType collaboratorType, OrganizationType organizationType, int userId)
        {
            foreach (var attendeeCollaborator in this.FindAllAttendeeCollaboratorsNotDeleted(edition))
            {
                attendeeCollaborator?.Delete(collaboratorType, organizationType, userId);
            }
        }

        /// <summary>Finds all attendee collaborators not deleted.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns></returns>
        private List<AttendeeCollaborator> FindAllAttendeeCollaboratorsNotDeleted(Edition edition)
        {
            return this.AttendeeCollaborators?.Where(ac => (edition == null || ac.EditionId == edition.Id) && !ac.IsDeleted)?.ToList();
        }

        /// <summary>
        /// Finds all attendee collaborators not deleted.
        /// </summary>
        /// <returns></returns>
        private List<AttendeeCollaborator> FindAllAttendeeCollaboratorsNotDeleted()
        {
            return this.AttendeeCollaborators?.Where(ac => !ac.IsDeleted)?.ToList();
        }

        #endregion

        #endregion

        #region Updates

        /// <summary>Updates the admin main information.</summary>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="email">The email.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        /// <param name="hasAnySpecialNeeds">if set to <c>true</c> [has any special needs].</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAdminMainInformation(
                CollaboratorType collaboratorType,
                string firstName,
                string lastNames,
                string email,
                string badge,
                string cellPhone,
                string phoneNumber,
                bool? sharePublicEmail,
                string publicEmail,
                bool isImageUploaded,
                bool isImageDeleted,
                List<CollaboratorJobTitle> jobTitles,
                List<CollaboratorMiniBio> miniBios,
                DateTime? birthDate,
                CollaboratorGender collaboratorGender,
                string collaboratorGenderAdditionalInfo,
                CollaboratorRole collaboratorRole,
                string collaboratorRoleAdditionalInfo,
                CollaboratorIndustry collaboratorIndustry,
                string collaboratorIndustryAdditionalInfo,
                bool hasAnySpecialNeeds,
                string specialNeedsDescription,
                bool? haveYouBeenToRio2CBefore,
                List<Edition> editionsParticipated,
                Edition edition,
                int userId,
                string companyName)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.UpdateUser(email);
            this.Badge = badge?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.CompanyName = companyName?.Trim();
            this.UpdatePublicEmail(sharePublicEmail, publicEmail);
            this.UpdateImageUploadDate(isImageUploaded, isImageDeleted);
            this.SynchronizeJobTitles(jobTitles, userId);
            this.SynchronizeMiniBios(miniBios, userId);
            this.UpdateEditions(haveYouBeenToRio2CBefore, editionsParticipated, userId);

            this.BirthDate = birthDate;
            this.UpdateGender(collaboratorGender, collaboratorGenderAdditionalInfo);
            this.UpdateRole(collaboratorRole, collaboratorRoleAdditionalInfo);
            this.UpdateIndustry(collaboratorIndustry, collaboratorIndustryAdditionalInfo);
            this.UpdateSpecialNeeds(hasAnySpecialNeeds, specialNeedsDescription);

            this.SetUpdateDate(userId);
        }

        /// <summary>Updates the site main information.</summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        /// <param name="hasAnySpecialNeeds">if set to <c>true</c> [has any special needs].</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateSiteMainInformation(
            string firstName,
            string lastNames,
            string badge,
            string cellPhone,
            string phoneNumber,
            bool? sharePublicEmail,
            string publicEmail,
            List<CollaboratorJobTitle> jobTitles,
            List<CollaboratorMiniBio> miniBios,
            DateTime? birthDate,
            CollaboratorGender collaboratorGender,
            string collaboratorGenderAdditionalInfo,
            CollaboratorRole collaboratorRole,
            string collaboratorRoleAdditionalInfo,
            CollaboratorIndustry collaboratorIndustry,
            string collaboratorIndustryAdditionalInfo,
            bool hasAnySpecialNeeds,
            string specialNeedsDescription,
            bool? haveYouBeenToRio2CBefore,
            List<Edition> editionsParticipated,
            Edition edition,
            bool isImageUploaded,
            int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.Badge = badge?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.UpdatePublicEmail(sharePublicEmail, publicEmail);
            this.UpdateImageUploadDate(isImageUploaded, false);
            this.UpdateEditions(haveYouBeenToRio2CBefore, editionsParticipated, userId);
            this.SynchronizeJobTitles(jobTitles, userId);
            this.SynchronizeMiniBios(miniBios, userId);
            this.OnboardAttendeeCollaboratorData(edition, userId);

            this.BirthDate = birthDate;
            this.UpdateGender(collaboratorGender, collaboratorGenderAdditionalInfo);
            this.UpdateRole(collaboratorRole, collaboratorRoleAdditionalInfo);
            this.UpdateIndustry(collaboratorIndustry, collaboratorIndustryAdditionalInfo);
            this.UpdateSpecialNeeds(hasAnySpecialNeeds, specialNeedsDescription);

            this.SetUpdateDate(userId);
        }

        /// <summary>Updates the address.</summary>
        /// <param name="country">The country.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="cityUid">The city uid.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="address1">The address1.</param>
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
            string addressZipCode,
            bool addressIsManual,
            int userId)
        {
            if (this.Address == null)
            {
                this.Address = new Address(
                    country,
                    stateUid,
                    stateName,
                    cityUid,
                    cityName,
                    address1,
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
                    addressZipCode,
                    addressIsManual,
                    userId);
            }
        }

        /// <summary>
        /// Updates the cell phone.
        /// </summary>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateCellPhone(
            string cellPhone,
            int userId)
        {
            this.CellPhone = cellPhone;

            this.SetUpdateDate(userId);
        }

        /// <summary>Updates the social networks.</summary>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateSocialNetworks(
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            int userId)
        {
            this.Website = website?.Trim();
            this.Linkedin = linkedin?.Trim();
            this.Twitter = twitter?.Trim();
            this.Instagram = instagram?.Trim();
            this.Youtube = youtube?.Trim();

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Updates the API configuration.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="isApiDisplayEnabled">if set to <c>true</c> [is API display enabled].</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateApiConfiguration(
            Edition edition,
            CollaboratorType collaboratorType,
            bool isApiDisplayEnabled,
            int? apiHighlightPosition,
            int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition?.Id ?? 0);
            attendeeCollaborator?.UpdateApiConfiguration(collaboratorType, isApiDisplayEnabled, apiHighlightPosition, userId);

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Updates the collaborator innovation organization tracks.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeInnovationOrganizationTracks">The attendee innovation organization tracks.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateCollaboratorInnovationOrganizationTracks(
            Edition edition,
            List<AttendeeInnovationOrganizationTrack> attendeeInnovationOrganizationTracks,
            int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition?.Id ?? 0);
            attendeeCollaborator?.SynchronizeAttendeeInnovationOrganizationTracks(attendeeInnovationOrganizationTracks, userId);
        }

        /// <summary>
        /// Updates the collaborator interests.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateCollaboratorInterests(
            Edition edition,
            ProjectType projectType,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition?.Id ?? 0);
            attendeeCollaborator?.UpdateAttendeeCollaboratorInterests(attendeeCollaboratorInterests, projectType, userId);
        }

        #region Privates

        /// <summary>
        /// Updates the special needs.
        /// </summary>
        /// <param name="hasAnySpecialNeeds">if set to <c>true</c> [has any special needs].</param>
        /// <param name="specialNeedsDescription">The special needs description.</param>
        private void UpdateSpecialNeeds(bool hasAnySpecialNeeds, string specialNeedsDescription)
        {
            this.HasAnySpecialNeeds = hasAnySpecialNeeds;
            this.SpecialNeedsDescription = hasAnySpecialNeeds ? specialNeedsDescription : null;
        }

        /// <summary>
        /// Updates the industry.
        /// </summary>
        /// <param name="collaboratorIndustry">The collaborator industry.</param>
        /// <param name="collaboratorIndustryAdditionalInfo">The collaborator industry additional information.</param>
        private void UpdateIndustry(CollaboratorIndustry collaboratorIndustry, string collaboratorIndustryAdditionalInfo)
        {
            this.Industry = collaboratorIndustry;
            this.CollaboratorIndustryId = collaboratorIndustry?.Id;
            this.CollaboratorIndustryAdditionalInfo = collaboratorIndustry?.HasAdditionalInfo ?? false ? collaboratorIndustryAdditionalInfo : null;
        }

        /// <summary>
        /// Updates the role.
        /// </summary>
        /// <param name="collaboratorRole">The collaborator role.</param>
        /// <param name="collaboratorRoleAdditionalInfo">The collaborator role additional information.</param>
        private void UpdateRole(CollaboratorRole collaboratorRole, string collaboratorRoleAdditionalInfo)
        {
            this.CollaboratorRoleId = collaboratorRole?.Id;
            this.Role = collaboratorRole;
            this.CollaboratorRoleAdditionalInfo = collaboratorRole?.HasAdditionalInfo ?? false ? collaboratorRoleAdditionalInfo : null;
        }

        /// <summary>
        /// Updates the gender.
        /// </summary>
        /// <param name="collaboratorGender">The collaborator gender.</param>
        /// <param name="collaboratorGenderAdditionalInfo">The collaborator gender additional information.</param>
        private void UpdateGender(CollaboratorGender collaboratorGender, string collaboratorGenderAdditionalInfo)
        {
            this.Gender = collaboratorGender;
            this.CollaboratorGenderId = collaboratorGender?.Id;
            this.CollaboratorGenderAdditionalInfo = collaboratorGender?.HasAdditionalInfo ?? false ? collaboratorGenderAdditionalInfo : null;
        }

        /// <summary>Updates the editions.</summary>
        /// <param name="haveYouBeenToRio2CBefore">The have you been to rio2 c before.</param>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="userId">The user identifier.</param>
        private void UpdateEditions(bool? haveYouBeenToRio2CBefore, List<Edition> editionsParticipated, int userId)
        {
            this.DeleteCollaboratorParticipation(editionsParticipated, userId);

            // No selection was made
            if (!haveYouBeenToRio2CBefore.HasValue)
            {
                return;
            }

            if (editionsParticipated == null || !editionsParticipated.Any())
            {
                return;
            }

            if (this.EditionParticipantions == null)
            {
                this.EditionParticipantions = new List<CollaboratorEditionParticipation>();
            }

            foreach (var edition in editionsParticipated)
            {
                var existing = EditionParticipantions.FirstOrDefault(e => e.EditionId == edition.Id);

                if (existing == null)
                {
                    EditionParticipantions.Add(new CollaboratorEditionParticipation(edition, this, userId));
                    continue;
                }

                if (existing.IsDeleted)
                {
                    existing.Undelete(userId);
                }
            }
        }

        /// <summary>Deletes the collaborator participation.</summary>
        /// <param name="editionsParticipated">The editions participated.</param>
        /// <param name="userId">The user identifier.</param>
        private void DeleteCollaboratorParticipation(List<Edition> editionsParticipated, int userId)
        {
            if (editionsParticipated == null)
            {
                editionsParticipated = new List<Edition>();
            }

            if (this.EditionParticipantions == null)
            {
                return;
            }

            foreach (var participation in this.EditionParticipantions)
            {
                if (editionsParticipated.All(e => e.Id != participation.Id && !e.IsDeleted))
                {
                    participation.Delete(userId);
                }
            }
        }

        /// <summary>Updates the public email.</summary>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        private void UpdatePublicEmail(bool? sharePublicEmail, string publicEmail)
        {
            if (!sharePublicEmail.HasValue)
            {
                return;
            }

            this.PublicEmail = sharePublicEmail == true ? publicEmail?.Trim() : null;
        }

        /// <summary>Updates the image upload date.</summary>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="isImageDeleted">if set to <c>true</c> [is image deleted].</param>
        private void UpdateImageUploadDate(bool isImageUploaded, bool isImageDeleted)
        {
            if (isImageUploaded)
            {
                this.ImageUploadDate = DateTime.UtcNow;
            }
            else if (isImageDeleted)
            {
                this.ImageUploadDate = null;
            }
        }

        #endregion

        #endregion

        #region Onboarding

        /// <summary>Onboards the specified edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void Onboard(Edition edition, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.Onboard(userId);
            this.SetUpdateDate(userId);
        }

        /// <summary>Called when [access data].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastNames">The last names.</param>
        /// <param name="badge">The badge.</param>
        /// <param name="cellPhone">The cell phone.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardAccessData(Edition edition, string firstName, string lastNames, string badge, string cellPhone, string phoneNumber, string passwordHash, int userId)
        {
            this.FirstName = firstName?.Trim();
            this.LastNames = lastNames?.Trim();
            this.Badge = badge?.Trim();
            this.PhoneNumber = phoneNumber?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.OnboardAttendeeCollaboratorAccessData(edition, userId);
            this.OnboardUser(passwordHash);

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>Onboard collaborator data.</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="sharePublicEmail">The share public email.</param>
        /// <param name="publicEmail">The public email.</param>
        /// <param name="isImageUploaded">if set to <c>true</c> [is image uploaded].</param>
        /// <param name="jobTitles">The job titles.</param>
        /// <param name="miniBios">The mini bios.</param>
        /// <param name="website">The website.</param>
        /// <param name="linkedin">The linkedin.</param>
        /// <param name="twitter">The twitter.</param>
        /// <param name="instagram">The instagram.</param>
        /// <param name="youtube">The youtube.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardData(
            Edition edition,
            bool? sharePublicEmail,
            string publicEmail,
            bool isImageUploaded,
            List<CollaboratorJobTitle> jobTitles,
            List<CollaboratorMiniBio> miniBios,
            DateTime? birthDate,
            CollaboratorGender collaboratorGender,
            string collaboratorGenderAdditionalInfo,
            CollaboratorRole collaboratorRole,
            string collaboratorRoleAdditionalInfo,
            CollaboratorIndustry collaboratorIndustry,
            string collaboratorIndustryAdditionalInfo,
            bool hasAnySpecialNeeds,
            string specialNeedsDescription,
            bool? haveYouBeenToRio2CBefore,
            List<Edition> editionsParticipated,
            string website,
            string linkedin,
            string twitter,
            string instagram,
            string youtube,
            int userId,
            string companyName)
        {
            this.UpdatePublicEmail(sharePublicEmail, publicEmail);
            this.UpdateImageUploadDate(isImageUploaded, false);
            this.SynchronizeJobTitles(jobTitles, userId);
            this.SynchronizeMiniBios(miniBios, userId);
            this.OnboardAttendeeCollaboratorData(edition, userId);
            this.UpdateEditions(haveYouBeenToRio2CBefore, editionsParticipated, userId);

            this.Website = website?.Trim();
            this.Linkedin = linkedin?.Trim();
            this.Twitter = twitter?.Trim();
            this.Instagram = instagram?.Trim();
            this.Youtube = youtube?.Trim();

            this.BirthDate = birthDate;
            this.Gender = collaboratorGender;
            this.Industry = collaboratorIndustry;
            this.Role = collaboratorRole;
            this.CollaboratorGenderId = collaboratorGender?.Id;
            this.CollaboratorGenderAdditionalInfo = collaboratorGenderAdditionalInfo;
            this.CollaboratorRoleId = collaboratorRole?.Id;
            this.CollaboratorRoleAdditionalInfo = collaboratorRoleAdditionalInfo;
            this.CollaboratorIndustryId = collaboratorIndustry?.Id;
            this.CollaboratorIndustryAdditionalInfo = collaboratorIndustryAdditionalInfo;
            this.HasAnySpecialNeeds = hasAnySpecialNeeds;
            this.SpecialNeedsDescription = specialNeedsDescription;
            this.CompanyName = companyName;

            this.SetUpdateDate(userId);
        }

        #region Music Player Executive

        /// <summary>
        /// Called when [music player data].
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorTargetAudiences">The attendee collaborator target audiences.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardMusicPlayerData(
            Edition edition,
            ProjectType projectType,
            List<AttendeeCollaboratorActivity> attendeeCollaboratorActivities,
            List<AttendeeCollaboratorTargetAudience> attendeeCollaboratorTargetAudiences,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.UpdateAttendeeCollaboratorActivities(attendeeCollaboratorActivities, projectType, userId);
            attendeeCollaborator?.UpdateAttendeeCollaboratorTargetAudiences(attendeeCollaboratorTargetAudiences, projectType, userId);
            attendeeCollaborator?.UpdateAttendeeCollaboratorInterests(attendeeCollaboratorInterests, projectType, userId);

            this.SetUpdateDate(userId);
        }

        /// <summary>
        /// Called when [music player terms acceptance].
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardMusicPlayerTermsAcceptance(Edition edition, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.OnboardMusicPlayerTermsAcceptance(userId);
            this.SetUpdateDate(userId);
        }


        /// <summary>
        /// Called when [music participant terms acceptance].
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardMusicProducerTermsAcceptanceDate(Edition edition, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.OnboardMusicProducerTermsAcceptanceDate(userId);
            this.SetUpdateDate(userId);
        }

        #endregion

        #region Innovation Player Executive

        /// <summary>
        /// Called when [innovation player data].
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <param name="attendeeCollaboratorActivities">The attendee collaborator activities.</param>
        /// <param name="attendeeCollaboratorInnovationOrganizationTracks">The attendee collaborator innovation organization tracks.</param>
        /// <param name="attendeeCollaboratorInterests">The attendee collaborator interests.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardInnovationPlayerData(
            Edition edition,
            ProjectType projectType,
            List<AttendeeCollaboratorActivity> attendeeCollaboratorActivities,
            List<AttendeeCollaboratorInnovationOrganizationTrack> attendeeCollaboratorInnovationOrganizationTracks,
            List<AttendeeCollaboratorInterest> attendeeCollaboratorInterests,
            int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.UpdateAttendeeCollaboratorActivities(attendeeCollaboratorActivities, projectType, userId);
            attendeeCollaborator?.UpdateAttendeeCollaboratorInnovationOrganizationTracks(attendeeCollaboratorInnovationOrganizationTracks, userId);
            attendeeCollaborator?.UpdateAttendeeCollaboratorInterests(attendeeCollaboratorInterests, projectType, userId);

            this.SetUpdateDate(userId);
        }

        /// <summary>
        /// Called when [innovation player terms acceptance].
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardInnovationPlayerTermsAcceptance(Edition edition, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.OnboardInnovationPlayerTermsAcceptance(userId);
            this.SetUpdateDate(userId);
        }

        #endregion

        #region Onboard Terms Acceptance

        /// <summary>
        /// Called when [audiovisual player terms acceptance].
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardAudiovisualPlayerTermsAcceptance(Edition edition, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.OnboardAudiovisualPlayerTermsAcceptance(userId);
            this.SetUpdateDate(userId);
        }

        /// <summary>Called when [speaker terms acceptance].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardSpeakerTermsAcceptance(Edition edition, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.OnboardSpeakerTermsAcceptance(userId);
            this.SetUpdateDate(userId);
        }

        /// <summary>Called when [[audiovisual producer business round terms acceptance].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardAudiovisualProducerBusinessRoundTermsAcceptance(Edition edition, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.OnboardAudiovisualProducerBusinessRoundTermsAcceptance(userId);
            this.SetUpdateDate(userId);
        }

        /// <summary>Called when [audiovisual producer pitching terms acceptance].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        public void OnboardAudiovisualProducerPitchingTermsAcceptance(Edition edition, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.OnboardAudiovisualProducerPitchingTermsAcceptance(userId);
            this.SetUpdateDate(userId);
        }

        #endregion

        /// <summary>
        /// Updates the welcome email send date.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateWelcomeEmailSendDate(int editionId, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(editionId);
            attendeeCollaborator?.UpdateWelcomeEmailSendDate(userId);
        }

        /// <summary>
        /// Updates the agenda email send date.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void UpdateAgendaEmailSendDate(int editionId, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(editionId);
            attendeeCollaborator?.UpdateAgendaEmailSendDate(userId);
        }

        #region Privates

        /// <summary>Called when [attendee collaborator access data].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        private void OnboardAttendeeCollaboratorAccessData(Edition edition, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            if (attendeeCollaborator != null)
            {
                attendeeCollaborator.OnboardAccessData(userId);
            }
            else
            {
                if (this.AttendeeCollaborators == null)
                {
                    this.AttendeeCollaborators = new List<AttendeeCollaborator>();
                }

                this.AttendeeCollaborators.Add(new AttendeeCollaborator(
                    edition,
                    new CollaboratorType(),
                    null,
                    null,
                    null,
                    this,
                    false,
                    userId)); //TODO: Onboard collaborator when the attendee collaborator does not exists (collaborator type)
            }
        }

        /// <summary>Called when [attendee collaborator data].</summary>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        private void OnboardAttendeeCollaboratorData(Edition edition, int userId)
        {
            var attendeeCollaborator = this.GetAttendeeCollaboratorByEditionId(edition.Id);
            attendeeCollaborator?.OnboardData(userId);
        }

        #endregion

        #endregion

        #region Helpers

        /// <summary>Gets the display name.</summary>
        /// <returns></returns>
        public string GetDisplayName()
        {
            return this.Badge ?? this.GetFullName();
        }

        /// <summary>Gets the display name abbreviation.</summary>
        /// <returns></returns>
        public string GetDisplayNameAbbreviation()
        {
            return this.GetDisplayName().GetTwoLetterCode();
        }

        /// <summary>Gets the full name.</summary>
        /// <returns></returns>
        public string GetFullName()
        {
            return this.FirstName + (!string.IsNullOrEmpty(this.LastNames) ? " " + this.LastNames : String.Empty);
        }

        /// <summary>Gets the name abbreviation.</summary>
        /// <returns></returns>
        public string GetNameAbbreviation()
        {
            return this.GetFullName().GetTwoLetterCode();
        }

        /// <summary>Determines whether this instance has image.</summary>
        /// <returns>
        ///   <c>true</c> if this instance has image; otherwise, <c>false</c>.</returns>
        public bool HasImage()
        {
            return this.ImageUploadDate.HasValue;
        }

        /// <summary>Gets the attendee collaborator by edition identifier.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public AttendeeCollaborator GetAttendeeCollaboratorByEditionId(int editionId)
        {
            return this.AttendeeCollaborators?.FirstOrDefault(ac => ac.EditionId == editionId);
        }

        /// <summary>
        /// Gets the attendee collaborator by edition identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public T GetAttendeeCollaboratorByEditionId<T>(int editionId) where T : AttendeeCollaborator
        {
            return this.AttendeeCollaborators?.FirstOrDefault(ac => ac.EditionId == editionId) as T;
        }

        #region Privates

        /// <summary>Finds all roles by attendee collaborator types.</summary>
        /// <returns></returns>
        private List<Role> FindAllRolesByAttendeeCollaboratorTypes()
        {
            return this.AttendeeCollaborators?
                            .Where(ac => !ac.IsDeleted)?
                            .SelectMany(ac => ac.AttendeeCollaboratorTypes
                                                    .Where(act => !act.IsDeleted && act.CollaboratorType?.IsDeleted == false)
                                                    .Select(act => act?.CollaboratorType?.Role))
                                                    ?.Distinct()
                                                    ?.ToList();
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
            this.ValidateDocument();
            this.ValidateBadge();
            this.ValidatePhoneNumber();
            this.ValidateCellPhone();
            this.ValidatePublicEmail();
            this.ValidateWebsite();
            this.ValidateLinkedin();
            this.ValidateTwitter();
            this.ValidateInstagram();
            this.ValidateYoutube();
            this.ValidateJobTitles();
            this.ValidateMiniBios();
            this.ValidateUser();
            this.ValidateAttendeeCollaborators();
            this.ValidateAddress();
            //this.ValidateGender();
            //this.ValidateIndustry();
            //this.ValidateRole();
            //this.ValidateBirthDate();

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
            //Disabled because some ticket sales platforms accepts only FirstName.
            //Last name is not required and this validation blocks the User registration by the sales platforms!
            //if (!string.IsNullOrEmpty(this.LastNames) && this.LastNames?.Trim().Length > LastNamesMaxLength)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.LastNames, LastNamesMaxLength, 1), new string[] { "LastNames" }));
            //}
        }

        /// <summary>Validates the document.</summary>
        public void ValidateDocument()
        {
            if (!string.IsNullOrEmpty(this.Document) && this.Document?.Trim().Length > DocumentMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.AllRegistrationDocuments, DocumentMaxLength, 1), new string[] { "Document" }));
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

        /// <summary>Validates the public email.</summary>
        public void ValidatePublicEmail()
        {
            if (!string.IsNullOrEmpty(this.PublicEmail) && this.PublicEmail?.Trim().Length > PublicEmailMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Email, PublicEmailMaxLength, 1), new string[] { "PublicEmail" }));
            }
        }

        /// <summary>Validates the website.</summary>
        public void ValidateWebsite()
        {
            if (!string.IsNullOrEmpty(this.Website) && this.Website?.Trim().Length > WebsiteMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.Website, WebsiteMaxLength, 1), new string[] { "Website" }));
            }
        }

        /// <summary>Validates the linkedin.</summary>
        public void ValidateLinkedin()
        {
            if (!string.IsNullOrEmpty(this.Linkedin) && this.Linkedin?.Trim().Length > LinkedinMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Linkedin", LinkedinMaxLength, 1), new string[] { "Linkedin" }));
            }
        }

        /// <summary>Validates the twitter.</summary>
        public void ValidateTwitter()
        {
            if (!string.IsNullOrEmpty(this.Twitter) && this.Twitter?.Trim().Length > TwitterMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Twitter", TwitterMaxLength, 1), new string[] { "Twitter" }));
            }
        }

        /// <summary>Validates the instagram.</summary>
        public void ValidateInstagram()
        {
            if (!string.IsNullOrEmpty(this.Instagram) && this.Instagram?.Trim().Length > InstagramMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Instagram", InstagramMaxLength, 1), new string[] { "Instagram" }));
            }
        }

        /// <summary>Validates the youtube.</summary>
        public void ValidateYoutube()
        {
            if (!string.IsNullOrEmpty(this.Youtube) && this.Youtube?.Trim().Length > YoutubeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Youtube", YoutubeMaxLength, 1), new string[] { "Youtube" }));
            }
        }

        /// <summary>Validates the birth date.</summary>
        private void ValidateBirthDate()
        {
            if (!BirthDate.HasValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.BirthDate), new string[] { "BirthDate" }));
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
            if (this.User != null && !this.User.IsDeleted && !this.User.IsValid())
            {
                this.ValidationResult.Add(this.User.ValidationResult);
            }
        }

        /// <summary>Validates the first name.</summary>
        public void ValidateGender()
        {
            if (string.IsNullOrEmpty(this.CollaboratorGenderAdditionalInfo?.Trim()) && Gender?.HasAdditionalInfo == true)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "CollaboratorGenderAdditionalInfo" }));
            }

            if ((this.CollaboratorGenderAdditionalInfo?.Trim().Length < 1 || this.CollaboratorGenderAdditionalInfo?.Trim().Length > SpecialNeedsDescriptionMaxLength) && Gender?.HasAdditionalInfo == true)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.AdditionalInfo, SpecialNeedsDescriptionMaxLength, 1), new string[] { "CollaboratorGenderAdditionalInfo" }));
            }
        }

        /// <summary>Validates the first name.</summary>
        public void ValidateIndustry()
        {
            if (string.IsNullOrEmpty(this.CollaboratorIndustryAdditionalInfo?.Trim()) && Industry?.HasAdditionalInfo == true)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "CollaboratorIndustryAdditionalInfo" }));
            }

            if ((this.CollaboratorIndustryAdditionalInfo?.Trim().Length < 1 || this.CollaboratorIndustryAdditionalInfo?.Trim().Length > SpecialNeedsDescriptionMaxLength) && Industry?.HasAdditionalInfo == true)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.AdditionalInfo, SpecialNeedsDescriptionMaxLength, 1), new string[] { "CollaboratorIndustryAdditionalInfo" }));
            }
        }

        /// <summary>Validates the first name.</summary>
        public void ValidateRole()
        {
            if (string.IsNullOrEmpty(this.CollaboratorRoleAdditionalInfo?.Trim()) && Role?.HasAdditionalInfo == true)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Name), new string[] { "CollaboratorRoleAdditionalInfo" }));
            }

            if ((this.CollaboratorRoleAdditionalInfo?.Trim().Length < 1 || this.CollaboratorRoleAdditionalInfo?.Trim().Length > SpecialNeedsDescriptionMaxLength) && Role?.HasAdditionalInfo == true)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.AdditionalInfo, SpecialNeedsDescriptionMaxLength, 1), new string[] { "CollaboratorRoleAdditionalInfo" }));
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

        /// <summary>
        /// checks speaker is able for publication in the API
        /// </summary>
        /// <returns></returns>
        public void FillRequiredFieldsToPublishToApi()
        {
            this.RequiredFieldsToPublish = new Dictionary<string, object> {
                { "Image", new { IsValid = this.ImageUploadDate != null, Message = Labels.Image } },
                { "FirstName", new { IsValid = this.FirstName != null, Message = Labels.FirstName } },
                { "LastNames", new { IsValid = this.LastNames != null, Message = Labels.LastNames } },
                { "Badge", new { IsValid = this.Badge != null, Message = Labels.BadgeName } },
            };

            foreach (var jobTitle in this.JobTitles)
            {
                var lang = jobTitle.Language.Code.GetSplittedWord('-', 0).ToUpper();
                this.RequiredFieldsToPublish.Add(
                    $"JobTitle_{lang}",
                    new
                    {
                        IsValid = jobTitle.Value != null,
                        Message = string.Format(Labels.JobTitleTranslated, lang)
                    }
                );
            }

            foreach (var minioBio in this.MiniBios)
            {
                var lang = minioBio.Language.Code.GetSplittedWord('-', 0).ToUpper();
                this.RequiredFieldsToPublish.Add(
                    $"MiniBio_{lang}",
                    new
                    {
                        IsValid = minioBio.Value != null,
                        Message = string.Format(Labels.MiniBioTranslated, lang)
                    }
                );
            }

            this.RequiredFieldsToPublish.Add(
                "SpeakerTermsAcceptanceDate",
                new
                {
                    IsValid = this.AttendeeCollaborators?.Where(ct => ct.SpeakerTermsAcceptanceDate != null)?.Any(),
                    Message = Messages.ImageAuthorizationForm
                }
            );
        }

        #endregion
    }
}