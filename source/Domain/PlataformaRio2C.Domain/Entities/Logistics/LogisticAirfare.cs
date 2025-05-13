// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
// ***********************************************************************
// <copyright file="LogisticAirfare.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Globalization;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>LogisticAirfare</summary>
    public class LogisticAirfare : Entity
    {
        public static readonly int FromMaxLength = 100;
        public static readonly int ToMaxLength = 100;
        public static readonly int TicketNumberMaxLength = 20;
        public static readonly int AdditionalInfoMaxLength = 1000;

        public int LogisticId { get; private set; }
        public bool IsNational { get; private set; }
        public bool IsArrival { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public string TicketNumber { get; private set; }
        public string AdditionalInfo { get; private set; }
        public DateTimeOffset DepartureDate { get; private set; }
        public DateTimeOffset ArrivalDate { get; private set; }
        public DateTimeOffset? TicketUploadDate { get; private set; }

        public virtual Logistic Logistic { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticAirfare"/> class.</summary>
        /// <param name="logistic">The logistic.</param>
        /// <param name="isNational">The is national.</param>
        /// <param name="isArrival">The is arrival.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="ticketNumber">The ticket number.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="stringDepartureDate">The string departure date.</param>
        /// <param name="stringArrivalDate">The string arrival date.</param>
        /// <param name="isTicketUploaded">if set to <c>true</c> [is ticket uploaded].</param>
        /// <param name="isTicketDeleted">if set to <c>true</c> [is ticket deleted].</param>
        /// <param name="userId">The user identifier.</param>
        public LogisticAirfare(
            Logistic logistic,
            bool? isNational,
            bool? isArrival,
            string from,
            string to,
            string ticketNumber,
            string additionalInfo,
            string stringDepartureDate,
            string stringArrivalDate,
            bool isTicketUploaded,
            bool isTicketDeleted,
            int userId)
        {
            this.Logistic = logistic;
            this.IsNational = isNational ?? false;
            this.IsArrival = isArrival ?? false;
            this.From = from?.Trim();
            this.To = to?.Trim();
            this.UpdateDepartureDate(stringDepartureDate);
            this.UpdateArrivalDate(stringArrivalDate);

            this.TicketNumber = ticketNumber?.Trim();
            this.UpdateTicketUploadDate(isTicketUploaded, isTicketDeleted);
            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="LogisticAirfare"/> class.</summary>
        protected LogisticAirfare()
        {
        }

        /// <summary>Updates the specified is national.</summary>
        /// <param name="isNational">The is national.</param>
        /// <param name="isArrival">The is arrival.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="ticketNumber">The ticket number.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="stringDepartureDate">The string departure date.</param>
        /// <param name="stringArrivalDate">The string arrival date.</param>
        /// <param name="isTicketUploaded">if set to <c>true</c> [is ticket uploaded].</param>
        /// <param name="isTicketDeleted">if set to <c>true</c> [is ticket deleted].</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            bool? isNational,
            bool? isArrival,
            string from,
            string to,
            string ticketNumber,
            string additionalInfo,
            string stringDepartureDate,
            string stringArrivalDate,
            bool isTicketUploaded,
            bool isTicketDeleted,
            int userId)
        {
            this.IsNational = isNational ?? false;
            this.IsArrival = isArrival ?? false;
            this.From = from?.Trim();
            this.To = to?.Trim();
            this.UpdateDepartureDate(stringDepartureDate);
            this.UpdateArrivalDate(stringArrivalDate);

            this.TicketNumber = ticketNumber?.Trim();
            this.UpdateTicketUploadDate(isTicketUploaded, isTicketDeleted);
            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            if (this.IsDeleted)
            {
                return;
            }

            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Private Methods

        /// <summary>Updates the departure date.</summary>
        /// <param name="stringDepartureDate">The string departure date.</param>
        private void UpdateDepartureDate(string stringDepartureDate)
        {
            if (string.IsNullOrEmpty(stringDepartureDate))
            {
                return;
            }

            var datePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            var timePattern = "HH:mm";//CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern.Replace(" tt", "");

            var departureDate = stringDepartureDate.ToDateTime($"{datePattern} {timePattern}");
            if (!departureDate.HasValue)
            {
                return;
            }

            this.DepartureDate = departureDate.Value.ToUtcTimeZone();
        }

        /// <summary>Updates the arrival date.</summary>
        /// <param name="stringArrivalDate">The string arrival date.</param>
        private void UpdateArrivalDate(string stringArrivalDate)
        {
            if (string.IsNullOrEmpty(stringArrivalDate))
            {
                return;
            }

            var datePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            var timePattern = "HH:mm";//CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern.Replace(" tt", "");

            var arrivalDate = stringArrivalDate.ToDateTime($"{datePattern} {timePattern}");
            if (!arrivalDate.HasValue)
            {
                return;
            }

            this.ArrivalDate = arrivalDate.Value.ToUtcTimeZone();
        }

        /// <summary>Updates the ticket upload date.</summary>
        /// <param name="isTicketUploaded">if set to <c>true</c> [is ticket uploaded].</param>
        /// <param name="isTicketDeleted">if set to <c>true</c> [is ticket deleted].</param>
        private void UpdateTicketUploadDate(bool isTicketUploaded, bool isTicketDeleted)
        {
            if (isTicketUploaded)
            {
                this.TicketUploadDate = DateTime.UtcNow;
            }
            else if (isTicketDeleted)
            {
                this.TicketUploadDate = null;
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

            this.ValidateLogistic();
            this.ValidatePlaces();
            this.ValidateDates();
            this.ValidateTicketNumber();
            this.ValidateAdditionalInfo();

            return this.ValidationResult.IsValid;
        }

        /// <summary>Validates the logistic.</summary>
        public void ValidateLogistic()
        {
            if (this.Logistic == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Request), new string[] { "LogisticUid" }));
            }
        }

        /// <summary>Validates the places.</summary>
        public void ValidatePlaces()
        {
            if (string.IsNullOrEmpty(this.From?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.FromPlace), new string[] { "From" }));
            }

            if (this.From?.Trim().Length > FromMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.FromPlace, FromMaxLength, 1), new string[] { "From" }));
            }

            if (string.IsNullOrEmpty(this.To?.Trim()))
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.ToPlace), new string[] { "To" }));
            }

            if (this.To?.Trim().Length > FromMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.ToPlace, ToMaxLength, 1), new string[] { "To" }));
            }

            if (this.To?.Trim() == this.From?.Trim())
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyDifferentFromProperty, Labels.ToPlace, Labels.FromPlace), new string[] { "To" }));
            }
        }

        /// <summary>Validates the dates.</summary>
        public void ValidateDates()
        {
            if (this.DepartureDate == DateTimeOffset.MinValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.DepartureDate), new string[] { "Departure" }));
            }

            if (this.ArrivalDate == DateTimeOffset.MinValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.ArrivalDate), new string[] { "Arrival" }));
            }

            if (this.DepartureDate >= this.ArrivalDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.ArrivalDate, Labels.DepartureDate), new string[] { "Arrival" }));
            }
        }

        /// <summary>Validates the ticket number.</summary>
        public void ValidateTicketNumber()
        {
            if (!string.IsNullOrEmpty(this.TicketNumber?.Trim()) && this.TicketNumber?.Trim().Length > TicketNumberMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.TicketNumber, TicketNumberMaxLength, 1), new string[] { "TicketNumber" }));
            }
        }

        /// <summary>Validates the additional information.</summary>
        public void ValidateAdditionalInfo()
        {
            if (!string.IsNullOrEmpty(this.AdditionalInfo?.Trim()) && this.AdditionalInfo?.Trim().Length > AdditionalInfoMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, Labels.AdditionalInfo, AdditionalInfoMaxLength, 1), new string[] { "AdditionalInfo" }));
            }
        }

        #endregion
    }
}