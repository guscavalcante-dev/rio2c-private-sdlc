// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-13-2020
// ***********************************************************************
// <copyright file="LogisticAccommodation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Globalization;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>LogisticAccommodation</summary>
    public class LogisticAccommodation : Entity
    {
        public static readonly int AdditionalInfoMaxLength = 1000;

        public int LogisticId { get; private set; }
        public int AttendeePlaceId { get; private set; }
        public DateTimeOffset CheckInDate { get; private set; }
        public DateTimeOffset CheckOutDate { get; private set; }
        public string AdditionalInfo { get; private set; }

        public virtual Logistic Logistic { get; private set; }
        public virtual AttendeePlace AttendeePlace { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticAccommodation"/> class.</summary>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="stringCheckInDate">The string check in date.</param>
        /// <param name="stringCheckOutDate">The string check out date.</param>
        /// <param name="attendeePlace">The attendee place.</param>
        /// <param name="logistic">The logistic.</param>
        /// <param name="userId">The user identifier.</param>
        public LogisticAccommodation(
            string additionalInfo, 
            string stringCheckInDate, 
            string stringCheckOutDate, 
            AttendeePlace attendeePlace, 
            Logistic logistic, 
            int userId)
        {
            this.LogisticId = logistic?.Id ?? 0;
            this.Logistic = logistic;
            this.AttendeePlaceId = attendeePlace?.Id ?? 0;
            this.AttendeePlace = attendeePlace;
            this.UpdateCheckInDate(stringCheckInDate);
            this.UpdateCheckOutDate(stringCheckOutDate);
            this.AdditionalInfo = additionalInfo;

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="LogisticAccommodation"/> class.</summary>
        protected LogisticAccommodation()
        {
        }

        /// <summary>Updates the specified additional information.</summary>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="stringCheckInDate">The string check in date.</param>
        /// <param name="stringCheckOutDate">The string check out date.</param>
        /// <param name="attendeePlace">The attendee place.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            string additionalInfo,
            string stringCheckInDate,
            string stringCheckOutDate,
            AttendeePlace attendeePlace, 
            int userId)
        {
            this.AttendeePlaceId = attendeePlace?.Id ?? 0;
            this.AttendeePlace = attendeePlace;
            this.UpdateCheckInDate(stringCheckInDate);
            this.UpdateCheckOutDate(stringCheckOutDate);
            this.AdditionalInfo = additionalInfo;

            this.IsDeleted = false;
            this.UpdateUserId = userId;
            this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Deletes the specified user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        public void Delete(int userId)
        {
            this.IsDeleted = true;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
        }

        #region Private Methods

        /// <summary>Updates the check in date.</summary>
        /// <param name="stringCheckInDate">The string check in date.</param>
        private void UpdateCheckInDate(string stringCheckInDate)
        {
            if (string.IsNullOrEmpty(stringCheckInDate))
            {
                return;
            }

            var datePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            var timePattern = "HH:mm";

            var checkInDate = stringCheckInDate.ToDateTime($"{datePattern} {timePattern}");
            if (!checkInDate.HasValue)
            {
                return;
            }

            this.CheckInDate = checkInDate.Value.ToUtcTimeZone();
        }

        /// <summary>Updates the check out date.</summary>
        /// <param name="stringCheckOutDate">The string check out date.</param>
        private void UpdateCheckOutDate(string stringCheckOutDate)
        {
            if (string.IsNullOrEmpty(stringCheckOutDate))
            {
                return;
            }

            var datePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            var timePattern = "HH:mm";

            var checkOutDate = stringCheckOutDate.ToDateTime($"{datePattern} {timePattern}");
            if (!checkOutDate.HasValue)
            {
                return;
            }

            this.CheckOutDate = checkOutDate.Value.ToUtcTimeZone();
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
            this.ValidatePlace();
            this.ValidateDates();
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

        /// <summary>Validates the place.</summary>
        public void ValidatePlace()
        {
            if (this.AttendeePlace == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.Hotel), new string[] { "PlaceId" }));
            }
        }

        /// <summary>Validates the dates.</summary>
        public void ValidateDates()
        {
            if (this.CheckInDate == DateTimeOffset.MinValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.CheckInDate), new string[] { "CheckInDate" }));
            }

            if (this.CheckOutDate == DateTimeOffset.MinValue)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.CheckOutDate), new string[] { "CheckOutDate" }));
            }

            if (this.CheckInDate >= this.CheckOutDate)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyGreaterThanProperty, Labels.CheckOutDate, Labels.CheckInDate), new string[] { "CheckOutDate" }));
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