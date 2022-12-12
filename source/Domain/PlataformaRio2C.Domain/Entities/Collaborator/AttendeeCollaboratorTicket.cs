// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-12-2022
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTicket.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>AttendeeCollaboratorTicket</summary>
    public class AttendeeCollaboratorTicket : Entity
    {
        public static readonly int SalesPlatformAttendeeIdMinLength = 1;
        public static readonly int SalesPlatformAttendeeIdMaxLength = 40;
        public static readonly int FirstNameMinLength = 1;
        public static readonly int FirstNameMaxLength = 100;
        public static readonly int LastNamesMinLength = 1;
        public static readonly int LastNamesMaxLength = 200;
        public static readonly int CellPhoneMinLength = 1;
        public static readonly int CellPhoneMaxLength = 50;
        public static readonly int JobTitleMinLength = 1;
        public static readonly int JobTitleMaxLength = 200;
        public static readonly int BarcodeMinLength = 1;
        public static readonly int BarcodeMaxLength = 40;
        public static readonly int TicketUrlMinLength = 1;
        public static readonly int TicketUrlMaxLength = 400;

        public int AttendeeCollaboratorId { get; private set; }
        public int AttendeeSalesPlatformTicketTypeId { get; private set; }
        public string SalesPlatformAttendeeId { get; private set; }
        public DateTimeOffset SalesPlatformUpdateDate { get; private set; }
        public string FirstName { get; private set; }
        public string LastNames { get; private set; }
        public string CellPhone { get; private set; }
        public string JobTitle { get; private set; }

        // Barcode
        public string Barcode { get; private set; }
        public bool IsBarcodePrinted { get; private set; }
        public bool IsBarcodeUsed { get; private set; }
        public DateTimeOffset? BarcodeUpdateDate { get; private set; }

        // Ticket Url
        public string TicketUrl { get; private set; }
        public bool IsTicketPrinted { get; private set; }
        public bool IsTicketUsed { get; private set; }
        public DateTimeOffset? TicketUpdateDate { get; private set; }

        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual AttendeeSalesPlatformTicketType AttendeeSalesPlatformTicketType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorTicket"/> class.
        /// </summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
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
        public AttendeeCollaboratorTicket(
            AttendeeCollaborator attendeeCollaborator, 
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
            this.AttendeeCollaborator = attendeeCollaborator;
            this.AttendeeSalesPlatformTicketType = attendeeSalesPlatformTicketType;
            this.SalesPlatformAttendeeId = salesPlatformAttendeeId;
            this.SalesPlatformUpdateDate = salesPlatformUpdateDate;
            this.FirstName = firstName?.Trim();
            this.LastNames = lastName?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.JobTitle = jobTitle?.Trim();

            // Barcode
            this.Barcode = barcode?.Trim();
            this.IsBarcodePrinted = isBarcodePrinted;
            this.IsBarcodeUsed = isBarcodeUsed;
            this.BarcodeUpdateDate = barcodeUpdateDate;

            // Ticket Url
            this.TicketUrl = ticketUrl?.Trim();
            this.IsTicketPrinted = isTicketPrinted;
            this.IsTicketUsed = isTicketUsed;
            this.TicketUpdateDate = ticketUpdateDate;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId= userId;
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTicket"/> class.</summary>
        protected AttendeeCollaboratorTicket()
        {
        }

        /// <summary>
        /// Updates the specified attendee sales platform ticket type.
        /// </summary>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
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
        public void Update(
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
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
            // Check if the update date is before the last update
            if (salesPlatformUpdateDate < this.SalesPlatformUpdateDate)
            {
                if (this.ValidationResult == null)
                {
                    this.ValidationResult = new ValidationResult();
                }

                this.ValidationResult.Add(new ValidationError($"Request is older than the last update (Current: {salesPlatformUpdateDate.ToString("yyyyMMddHHmmss")}; Last: {this.SalesPlatformUpdateDate.ToString("yyyyMMddHHmmss")})."));
                return;
            }

            this.SalesPlatformUpdateDate = salesPlatformUpdateDate;
            this.AttendeeSalesPlatformTicketType = attendeeSalesPlatformTicketType;
            this.FirstName = firstName?.Trim();
            this.LastNames = lastName?.Trim();
            this.CellPhone = cellPhone?.Trim();
            this.JobTitle = jobTitle?.Trim();

            // Barcode
            this.Barcode = barcode?.Trim();
            this.IsBarcodePrinted = isBarcodePrinted;
            this.IsBarcodeUsed = isBarcodeUsed;
            this.BarcodeUpdateDate = barcodeUpdateDate;

            // Ticket Url
            this.TicketUrl = ticketUrl?.Trim();
            this.IsTicketPrinted = isTicketPrinted;
            this.IsTicketUsed = isTicketUsed;
            this.TicketUpdateDate = ticketUpdateDate;

            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        /// <summary>
        /// Deletes the specified sales platform update date.
        /// </summary>
        /// <param name="salesPlatformUpdateDate">The sales platform update date.</param>
        /// <param name="barcodeUpdateDate">The barcode update date.</param>
        /// <param name="ticketUpdateDate">The ticket update date.</param>
        /// <param name="userId">The user identifier.</param>
        public void Delete(
            DateTime salesPlatformUpdateDate,
            DateTime? barcodeUpdateDate,
            DateTime? ticketUpdateDate,
            int userId)
        {
            if (barcodeUpdateDate.HasValue && barcodeUpdateDate.Value < this.BarcodeUpdateDate)
            {
                if (this.ValidationResult == null)
                {
                    this.ValidationResult = new ValidationResult();
                }

                this.ValidationResult.Add(new ValidationError($"Barcode update is older than the last update (Current: {salesPlatformUpdateDate.ToString("yyyyMMddHHmmss")}; Last: {this.SalesPlatformUpdateDate.ToString("yyyyMMddHHmmss")})."));
                return;
            }

            if (ticketUpdateDate.HasValue && ticketUpdateDate.Value < this.TicketUpdateDate)
            {
                if (this.ValidationResult == null)
                {
                    this.ValidationResult = new ValidationResult();
                }

                this.ValidationResult.Add(new ValidationError($"Ticket update is older than the last update (Current: {salesPlatformUpdateDate.ToString("yyyyMMddHHmmss")}; Last: {this.SalesPlatformUpdateDate.ToString("yyyyMMddHHmmss")})."));
                return;
            }

            if (this.IsDeleted)
            {
                return;
            }

            this.SalesPlatformUpdateDate = salesPlatformUpdateDate;
            this.BarcodeUpdateDate = barcodeUpdateDate;
            this.TicketUpdateDate = ticketUpdateDate;
            base.Delete(userId);
        }

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

            this.ValidateSalesPlatformAttendeeId();
            this.ValidateFirstName();
            this.ValidateLastNames();
            this.ValidateCellPhone();
            this.ValidateJobTitle();
            this.ValidateBarcode();
            this.ValidateTicketUrl();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the sales platform attendee identifier.</summary>
        public void ValidateSalesPlatformAttendeeId()
        {
            if (string.IsNullOrEmpty(this.SalesPlatformAttendeeId?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Attendee Id"), new string[] { "SalesPlatformAttendeeId" }));
            }

            if (this.SalesPlatformAttendeeId?.Trim().Length < SalesPlatformAttendeeIdMinLength || this.SalesPlatformAttendeeId?.Trim().Length > SalesPlatformAttendeeIdMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Attendee Id", SalesPlatformAttendeeIdMaxLength, SalesPlatformAttendeeIdMinLength), new string[] { "SalesPlatformAttendeeId" }));
            }
        }

        /// <summary>Validates the first name.</summary>
        public void ValidateFirstName()
        {
            if (string.IsNullOrEmpty(this.FirstName?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, "Ticket Class"), new string[] { "FirstName" }));
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
            //if (this.LastNames?.Trim().Length < LastNamesMinLength || this.LastNames?.Trim().Length > LastNamesMaxLength)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.LastNames, LastNamesMaxLength, LastNamesMinLength), new string[] { "LastNames" }));
            //}
        }

        /// <summary>Validates the cell phone.</summary>
        public void ValidateCellPhone()
        {
            if (!string.IsNullOrEmpty(this.CellPhone) && (this.CellPhone?.Trim().Length < CellPhoneMinLength || this.CellPhone?.Trim().Length > CellPhoneMaxLength)) 
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.CellPhone, CellPhoneMaxLength, CellPhoneMinLength), new string[] { "CellPhone" }));
            }
        }

        /// <summary>Validates the job title.</summary>
        public void ValidateJobTitle()
        {
            if (!string.IsNullOrEmpty(this.JobTitle) && (this.JobTitle?.Trim().Length < JobTitleMinLength ||  this.JobTitle?.Trim().Length > JobTitleMaxLength))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.JobTitle, JobTitleMaxLength, JobTitleMinLength), new string[] { "JobTitle" }));
            }
        }

        /// <summary>Validates the barcode.</summary>
        public void ValidateBarcode()
        {
            if (this.Barcode?.Trim().Length < BarcodeMinLength || this.Barcode?.Trim().Length > BarcodeMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, "Barcode", BarcodeMaxLength, BarcodeMinLength), new string[] { "Barcode" }));
            }
        }

        /// <summary>Validates the ticket URL.</summary>
        public void ValidateTicketUrl()
        {
            if (this.TicketUrl?.Trim().Length < TicketUrlMinLength || this.TicketUrl?.Trim().Length > TicketUrlMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(this.TicketUrl), TicketUrlMaxLength, TicketUrlMinLength), new string[] { nameof(this.TicketUrl) }));
            }
        }

        #endregion
    }
}