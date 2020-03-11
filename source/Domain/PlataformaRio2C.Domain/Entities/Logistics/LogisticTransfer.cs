// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="LogisticTransfer.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>LogisticTransfer</summary>
    public class LogisticTransfer : Entity
    {
        public static readonly int AdditionalInfoMaxLength = 1000;

        public int LogisticId { get; private set; }
        public int FromAttendeePlaceId { get; private set; }
        public int ToAttendeePlaceId { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public string AdditionalInfo { get; private set; }
        public int? LogisticTransferStatusId { get; private set; }

        public virtual Logistic Logistic { get; private set; }
        public virtual AttendeePlace FromAttendeePlace { get; private set; }
        public virtual AttendeePlace ToAttendeePlace { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticTransfer"/> class.</summary>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="date">The date.</param>
        /// <param name="fromAttendeePlace">From attendee place.</param>
        /// <param name="toAttendeePlace">To attendee place.</param>
        /// <param name="logistic">The logistic.</param>
        /// <param name="userId">The user identifier.</param>
        public LogisticTransfer(string additionalInfo, DateTime? date, AttendeePlace fromAttendeePlace, AttendeePlace toAttendeePlace, Logistic logistic, int userId)
        {
            this.Logistic = logistic;
            this.FromAttendeePlaceId = fromAttendeePlace?.Id ?? 0;
            this.FromAttendeePlace = fromAttendeePlace;
            this.ToAttendeePlaceId = toAttendeePlace?.Id ?? 0;
            this.ToAttendeePlace = toAttendeePlace;

            if (date.HasValue)
            {
                this.Date = date.Value.ToUtcTimeZone();
            }

            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.CreateUserId = this.UpdateUserId = userId;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
        }

        /// <summary>Initializes a new instance of the <see cref="LogisticTransfer"/> class.</summary>
        protected LogisticTransfer()
        {
        }

        /// <summary>Updates the specified additional information.</summary>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="date">The date.</param>
        /// <param name="fromAttendeePlace">From attendee place.</param>
        /// <param name="toAttendeePlace">To attendee place.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string additionalInfo, DateTime date, AttendeePlace fromAttendeePlace, AttendeePlace toAttendeePlace, int userId)
        {
            this.FromAttendeePlaceId = fromAttendeePlace?.Id ?? 0;
            this.FromAttendeePlace = fromAttendeePlace;
            this.ToAttendeePlaceId = toAttendeePlace?.Id ?? 0;
            this.ToAttendeePlace = toAttendeePlace;
            this.Date = date.ToUtcTimeZone();
            this.AdditionalInfo = additionalInfo?.Trim();

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
            if (this.FromAttendeePlace == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.FromPlace), new string[] { "FromAttendeePlaceId" }));
            }

            if (this.ToAttendeePlace == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, Labels.ToPlace), new string[] { "ToAttendeePlaceId" }));
            }

            if (this.FromAttendeePlaceId == this.ToAttendeePlaceId)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyDifferentFromProperty, Labels.ToPlace, Labels.FromPlace), new string[] { "ToAttendeePlaceId" }));
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